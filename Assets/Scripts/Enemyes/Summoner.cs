using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Summoner : MonoBehaviour
{
    // External parameters
    [SerializeField]
    private GameObject instanceExample;
    public event EventHandler raise;

    // Internal parameters
    [SerializeField]
    int maxObjectsCount = 30;
    [SerializeField]
    float interval = 2f;
    float verticalHeightBoundsSize = 1.45f;
    float noInstNearPlayerTimeSpeedUp = 20f;
   


    // Buffers
    Queue<GameObject> reusePool;
    int instancesCount = 0;
    float requestedSummonerTimeScale = 1f;

    private void Start()
    {
        reusePool = new Queue<GameObject>(maxObjectsCount);
        StartCoroutine(CreateInstance());
    }

    private void Update()
    {
        if (Wall.ResponsCount > maxObjectsCount)
        {
            if (Wall.WallNearPlayer)
            {

                raise?.Invoke(gameObject, new ChangeTimeArgs(1f));
                Wall.ResetNearPlayerChecker();
            }
            else
            {
                raise?.Invoke(gameObject, new ChangeTimeArgs(noInstNearPlayerTimeSpeedUp));
                Wall.ResetNearPlayerChecker();
            }
        }
    }

    IEnumerator CreateInstance()
    {
        if (instancesCount < maxObjectsCount)
        {
            PerformInstantiate();
        }
        else
        {
            PerformReuse();
        }

        yield return new WaitForSeconds(interval);

        StartCoroutine(CreateInstance());
    }

    void PerformInstantiate()
    {
        Vector3 position = transform.position;
        position.y = UnityEngine.Random.value * verticalHeightBoundsSize * 2 - verticalHeightBoundsSize;
        GameObject instance = Instantiate(instanceExample, position, Quaternion.identity, gameObject.transform);
        InstanceSpeaker instanceSpeaker = instance.GetComponent<InstanceSpeaker>();

        instanceSpeaker.sendReusePoolRequest += TransitToReusePool;
        instancesCount++;
    }

    void PerformReuse()
    {
        if (reusePool.Count > 0)
        {
            Vector3 position = transform.position;
            position.y = UnityEngine.Random.value * verticalHeightBoundsSize * 2 - verticalHeightBoundsSize;
            GameObject instance = reusePool.Dequeue();
            instance.SetActive(true);
            instance.transform.position = position;
        }
    }

    public void TransitToReusePool(GameObject instance)
    {
        reusePool.Enqueue(instance);
    }
}

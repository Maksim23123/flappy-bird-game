using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartDestroyTimer());
    }

    IEnumerator StartDestroyTimer()
    {
        yield return new WaitForSecondsRealtime(lifeTime);
        Destroy(gameObject);
    }
}

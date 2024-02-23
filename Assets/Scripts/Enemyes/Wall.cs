using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // External parameters

    [SerializeField]
    InstanceSpeaker instanceSpeaker;
    
    // Internal parameters
    private static bool wallNearPlayer = false;
    public static bool WallNearPlayer
    {
        get { return wallNearPlayer; }
    }

    private static int responsCount = 0;
    public static int ResponsCount
    {
        get { return responsCount; }
    }

    float speed = 5.0f;
    float retirePosition = -13f;
    float nearPlayerPosition = 26f;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < retirePosition)
        {
            instanceSpeaker.sendReusePoolRequest?.Invoke(gameObject);
            gameObject.SetActive(false);
        }
        else if (transform.position.x < nearPlayerPosition)
        {
            wallNearPlayer = true;
        }
        responsCount += 1;
    }

    public static void ResetNearPlayerChecker()
    {
        responsCount = 0;
        wallNearPlayer = false;
    }
}

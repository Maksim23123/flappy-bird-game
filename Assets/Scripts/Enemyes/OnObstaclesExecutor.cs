using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnObstaclesExecutor : MonoBehaviour
{

    private void Start()
    {
        Services.OnObstaclesExecutor = this;
    }
    public void TryForceReuseObject(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out ObstacleMainElementReference instanceReferenceContainer))
        {
            Instance instance = instanceReferenceContainer.MainElement;
            instance.OnForceRestart();
        }
    }
}

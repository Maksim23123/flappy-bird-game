using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Instance : MonoBehaviour
{
    // External parameters

    [SerializeField]
    public Transform pivotPoint;
    public Action<GameObject> sendReuseRequest = null;
    

    // Internal parameters
    public float retireDistance = 13f;

    private void Update()
    {
        if (transform.position.x < pivotPoint.position.x - retireDistance)
        {
            sendReuseRequest?.Invoke(gameObject);
        }
    }

    public virtual void OnRestartGame(Func<Vector3, bool> validatePosition)
    {
        if (!validatePosition(transform.position))
        {
            sendReuseRequest?.Invoke(gameObject);
        }
    }

    public virtual void OnForceRestart()
    {
        sendReuseRequest?.Invoke(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToLookAtPoint : MonoBehaviour
{
    [SerializeField]
    Transform lookAtPoint;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = lookAtPoint.position - transform.position;
        Vector3 currentDirection = Vector3.RotateTowards(transform.forward, direction, Mathf.PI / 4, 1);


        Quaternion rotation = Quaternion.LookRotation(currentDirection, Vector3.up);
        transform.rotation = rotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField]
    private Transform followedObject;

    [SerializeField]
    private bool x, y, z;

    private float primalDifferenceX, primalDifferenceY, primalDifferenceZ;

    private void Start()
    {
        primalDifferenceX = transform.position.x - followedObject.position.x;
        primalDifferenceY = transform.position.y - followedObject.position.y;
        primalDifferenceZ = transform.position.z - followedObject.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;

        if (x)
            newPosition.x = followedObject.position.x + primalDifferenceX;
        if (y)
            newPosition.y = followedObject.position.y + primalDifferenceY;
        if (z)
            newPosition.z = followedObject.position.z + primalDifferenceZ;
        transform.position = newPosition;
    }
}

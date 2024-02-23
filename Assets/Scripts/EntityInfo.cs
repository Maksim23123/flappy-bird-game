using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using UnityEngine;


[CreateAssetMenu(fileName = "EntityInfo", menuName = "MyAssets/EntityInfo")]
public class EntityInfo : ScriptableObject
{
    public GameObject entity;

    [SerializeField]
    private EntityCoordinats entityCoordinats;

    [Space(10)]
    [SerializeField]
    private int instancesCount;
    [SerializeField]
    private float retireDistance;
    [SerializeField]
    private float minXSpawnDistance;

    public EntityCoordinats EntityCoordinats { get => entityCoordinats; }
    public int InstancesCount { get => instancesCount; }
    public float MinSpawnDistance { get => minXSpawnDistance; }
    public float RetireDistance { get => retireDistance; }

    public bool ValidatePosition(Vector3 position, Vector3 pivot)
    {
        float distanceX = position.x - pivot.x;
        if (distanceX < retireDistance || Mathf.Abs(distanceX) < minXSpawnDistance) 
            return false;
        else 
            return true;
    }
}

[Serializable]
public class EntityCoordinats
{
    [Header("Offset Parameters")]
    [Space(5)]
    [SerializeField]
    private float xOffset;
    [SerializeField]
    private float yOffset;
    [SerializeField]
    private float zOffset;

    [Space(10)]
    [Header("Range parameters")]
    [Space(5)]
    [SerializeField]
    private float xRangeValue;
    [SerializeField]
    private float yRangeValue;
    [SerializeField]
    private float zRangeValue;

    public float XOffset { get => xOffset; }
    public float YOffset { get => yOffset; }
    public float ZOffset { get => zOffset; }
    public float XRangeValue { get => xRangeValue; }
    public float YRangeValue { get => yRangeValue; }
    public float ZRangeValue { get => zRangeValue; }

    public Vector3 ProcessVector3(Vector3 vector, Func<float, float> rangeProcessor = null)
    {
        if (rangeProcessor == null)
        {
            rangeProcessor = RandomRangeProcessor;
        }

        float newXValue = vector.x + xOffset;
        newXValue = newXValue - xRangeValue / 2 + rangeProcessor(xRangeValue);

        float newYValue = vector.y + yOffset;
        newYValue = newYValue - yRangeValue / 2 + rangeProcessor(yRangeValue);

        float newZValue = vector.z + zOffset;
        newZValue = newZValue - zRangeValue / 2 + rangeProcessor(zRangeValue);

        return new Vector3(newXValue, newYValue, newZValue);
    }

    static float RandomRangeProcessor(float rangeValue)
    {
        return UnityEngine.Random.value * rangeValue;
    }
}




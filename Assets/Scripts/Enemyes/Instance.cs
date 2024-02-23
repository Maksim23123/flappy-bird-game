using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Instance : ScriptableObject
{
    public GameObject instance;

    public float interval;
    int maxObjecCount;
}

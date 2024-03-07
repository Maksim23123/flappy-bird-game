using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityInfoManager : MonoBehaviour
{
    public abstract void InstantiateAll();

    public abstract void Restart();
}

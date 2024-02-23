using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceSpeaker : MonoBehaviour
{
    public Action<GameObject> sendReusePoolRequest = null;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPlayerPrefsStats : MonoBehaviour
{
    [SerializeField]
    SerializableDict<string, int> keysValues;

    // Start is called before the first frame update
    void Start()
    {
        if (keysValues != null)
        {
            foreach (string key in keysValues.Keys)
            {
                PlayerPrefs.SetInt(key, keysValues[key]);
            }
        }
    }
}

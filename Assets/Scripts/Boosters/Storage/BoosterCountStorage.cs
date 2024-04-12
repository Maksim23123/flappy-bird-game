using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BoosterCountStorage
{
    [SerializeField]
    string key = "deffault";

    public int Value
    {
        get
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key);
            }
            return 0;
        }

        set 
        { 
            PlayerPrefs.SetInt(key, value);
        }
    }

    public bool Use()
    {
        if (PlayerPrefs.HasKey(key) && PlayerPrefs.GetInt(key) > 0)
        {
            PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) - 1);
            return true;
        }
        return false;
    }
}

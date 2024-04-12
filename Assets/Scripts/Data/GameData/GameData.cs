using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject
{
    
    private readonly string COINS_TAG = "coinsCount";

    
    private readonly string GEMS_TAG = "gemsCount";

    public int CoinsCount
    {
        get
        {
            if (PlayerPrefs.HasKey(COINS_TAG))
                return PlayerPrefs.GetInt(COINS_TAG);
            else
                return 0;
        }

        set
        {
            PlayerPrefs.SetInt(COINS_TAG, value);
        }
    }

    public int GemsCount
    {
        get
        {
            if (PlayerPrefs.HasKey(GEMS_TAG))
                return PlayerPrefs.GetInt(GEMS_TAG);
            else
                return 0;
        }

        set
        {
            PlayerPrefs.SetInt(GEMS_TAG, value);
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject
{

    private int coinsCount = 0;

    private int gemsCount = 0;

    public int CoinsCount
    {
        get => coinsCount;
        set
        {
            coinsCount = value;
        }
    }

    public int GemsCount
    {
        get => gemsCount;
        set
        {
            gemsCount = value;
        }
    }
}


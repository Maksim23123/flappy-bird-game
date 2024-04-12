using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AboveGameDataLayer
{
    public event EventHandler<CurrencyEventArgs> coinsCountChanged;
    public event EventHandler<CurrencyEventArgs> gemsCountChanged;

    [SerializeField]
    GameData gameData;

    public int CoinsCount
    {
        get
        {
            return gameData.CoinsCount;
        }

        set
        {
            gameData.CoinsCount = value;
            coinsCountChanged?.Invoke(this, new CurrencyEventArgs(CurrencyType.Coin, value));
        }
    }

    public int GemsCount
    {
        get
        {
            return gameData.GemsCount;
        }

        set
        {
            gameData.GemsCount = value;
            gemsCountChanged?.Invoke(this, new CurrencyEventArgs(CurrencyType.Gem, value));
        }
    }
}

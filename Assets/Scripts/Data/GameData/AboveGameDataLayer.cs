using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AboveGameDataLayer
{
    public event EventHandler<CurrencyEventArgs> currencyCountChanged;

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
            currencyCountChanged?.Invoke(this, new CurrencyEventArgs(CurrencyType.Coin, value));
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
            currencyCountChanged?.Invoke(this, new CurrencyEventArgs(CurrencyType.Gem, value));
        }
    }

    public int GetCurrencyAmountByType(CurrencyType type)
    {
        if (type == CurrencyType.Gem)
        {
            return GemsCount;
        }
        else
        {
            return CoinsCount;
        }
        
    }
}

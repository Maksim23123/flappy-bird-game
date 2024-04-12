using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyEventArgs : EventArgs
{
    public CurrencyType Type { get; }

    public int Value { get; set; }

    public CurrencyEventArgs(CurrencyType currencyType, int value)
    {
        Type = currencyType;
        Value = value;
    }

}

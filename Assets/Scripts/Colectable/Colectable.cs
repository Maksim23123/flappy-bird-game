using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Colectable : MonoBehaviour
{
    public int value = 1;
    protected bool multipliable = false;
    public event EventHandler<PickUpColectableEventArgs> PickUpColectable;

    protected void PickUpThis(ColectableType colectableType, int value)
    {
        float localMultiplier = 1;
        if (multipliable)
            localMultiplier = RoundStats.CurrencyesMultiplier;
        PickUpColectable?.Invoke(this, new PickUpColectableEventArgs(colectableType, value * (int)localMultiplier));
    }
}

public class PickUpColectableEventArgs : EventArgs
{
    public int ColectableCount { get; }
    public ColectableType Type { get; }

    public PickUpColectableEventArgs(ColectableType type, int coinsCount)
    {
        ColectableCount = coinsCount;
        Type = type;
    }
}

public enum ColectableType
{
    Coin,
    Gem
}

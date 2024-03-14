using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Colectable : MonoBehaviour
{
    public int value = 1;
    public event EventHandler<PickUpColectableEventArgs> PickUpColectable;

    protected void PickUpThis(PickUpColectableEventArgs e)
    {
        PickUpColectable?.Invoke(this, e);
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

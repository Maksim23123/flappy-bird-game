using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierBooster : Booster
{
    public float multiplierBoostedValue = 2;
    public float multiplierDeffaultValue = 1;

    public override void ApplyBoost()
    {
        if (buttonManager.UseBooster())
        {
            base.ApplyBoost();
            RoundStats.CurrencyesMultiplier = multiplierBoostedValue;
        }
    }

    public override void ForceReset()
    {
        ResetBooster();
        RoundStats.CurrencyesMultiplier = multiplierDeffaultValue;
    }
}

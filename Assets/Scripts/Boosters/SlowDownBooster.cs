using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SlowDownBooster : Booster
{
    public float speedMultiplier = 0.75f;


    public override void ApplyBoost()
    {
        if (buttonManager.UseBooster())
        {
            base.ApplyBoost();
            Services.PlayerExecutor.PlayerSpeed = Services.PlayerExecutor.PlayerSpeed * speedMultiplier;
        }
    }


    public override void ForceReset()
    {
        ResetBooster();
        Services.PlayerExecutor.ResetPlayerSpeed();
    }

}

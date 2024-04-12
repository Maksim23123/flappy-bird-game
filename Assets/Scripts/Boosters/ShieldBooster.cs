using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBooster : Booster
{

    public override void ApplyBoost()
    {
        if (buttonManager.UseBooster())
        {
            base.ApplyBoost();
            Services.PlayerExecutor.SetPlayerState(PlayerStates.Invulnerable);
        }
    }

    public override void ForceReset()
    {
        ResetBooster();
        Services.PlayerExecutor.SetPlayerState(PlayerStates.Deffault);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Booster : MonoBehaviour
{
    public float timeStep = 1;
    public float boostDuration = 10;
    protected float currentTime = 0;

    [SerializeField]
    protected BoosterButtonManager buttonManager;

    public virtual void ApplyBoost() 
    {
        currentTime = 0;
        buttonManager.IsBlocked = true;

        UpdateBoosterManager();

        StartCoroutine(BoostTimer());
    }

    public abstract void ForceReset();

    protected IEnumerator BoostTimer()
    {
        while (currentTime < boostDuration)
        {
            yield return new WaitForSecondsRealtime(timeStep);
            currentTime += timeStep;
            UpdateBoosterManager();
        }
        ForceReset();
    }

    protected void UpdateBoosterManager()
    {
        buttonManager.Time = (int)(boostDuration - currentTime);
        buttonManager.GrayCoverProgress = (boostDuration - currentTime) / boostDuration;
    }

    protected void ResetBooster()
    {
        buttonManager.IsBlocked = false;
        buttonManager.Time = -1;
        buttonManager.GrayCoverProgress = 0;
    }
}

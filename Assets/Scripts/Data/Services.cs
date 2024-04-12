using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Services
{
    private static OnObstaclesExecutor onObstaclesExecutor;

    private static PlayerExecutor playerExecutor;

    public static OnObstaclesExecutor OnObstaclesExecutor 
    { 
        get => onObstaclesExecutor; 
        set
        {
            if (onObstaclesExecutor != null && value != onObstaclesExecutor)
                throw new System.Exception("Multiple initialization error. Class name: " + typeof(OnObstaclesExecutor));
            else
                onObstaclesExecutor = value;
        }
    }

    public static PlayerExecutor PlayerExecutor { 
        get => playerExecutor; 
        set
        {
            if (playerExecutor != null && value != playerExecutor)
                throw new System.Exception("Multiple initialization error. Class name: " + typeof(OnObstaclesExecutor));
            else
                playerExecutor = value;
        }
    }
}

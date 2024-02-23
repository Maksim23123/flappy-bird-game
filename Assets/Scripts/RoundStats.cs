using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoundStats
{
    static private int gameScore = 0;

    static public int GameScore
    {
        get { return gameScore; }
    }

    static public void ResetGameScore()
    {
        gameScore = 0;
    }

    static public void ImproveScore()
    {
        gameScore++;
    }
}

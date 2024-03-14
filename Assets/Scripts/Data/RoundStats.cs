using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoundStats
{
    static private int gameScore = 0;

    static private Difficulty currentDifficulty = Difficulty.Stage1;

    static public event Action<Difficulty> DifficultyChanged = null;

    static public event Action<int> GameScoreChanged = null;

    static public Vector3 firstEnemyPosition;

    static public int GameScore
    {
        get { return gameScore; }
    }
    static private int InternalGameScore
    {
        get { return gameScore; }

        set {
            gameScore = value;
            GameScoreChanged(value);
        }
    }

    public static Difficulty CurrentDificulty
    {
        get => currentDifficulty;

        set
        {
            currentDifficulty = value;
            DifficultyChanged?.Invoke(value);
        }
    }

    static public void ResetGameScore()
    {
        InternalGameScore = 0;
    }

    static public void ImproveScore()
    {
        InternalGameScore++;
    }

    public enum Difficulty
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        Stage5,
        StageInfinity
    }
}

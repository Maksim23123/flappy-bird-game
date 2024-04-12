using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    public LevelManager levelManager;
    public Booster[] boosters;

    private void Start()
    {
        levelManager.onChangeGameState += OnChangeGameState;
    }

    private void OnChangeGameState(GameState gameState)
    {
        if (gameState == GameState.GameOver)
        {
            foreach (var booster in boosters)
            {
                booster.ForceReset();
            }
        }
    }
}

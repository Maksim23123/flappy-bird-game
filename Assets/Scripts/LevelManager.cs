using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public event Action<GameState> onChangeGameState = null;

    private GameState gameState = GameState.Game;
    public GameState GameState
    {
        get { return gameState; }

        set {
            onChangeGameState?.Invoke(value);
            gameState = value; 
        }
    }

    [SerializeField]
    private GameObject summoner;
    [SerializeField]
    private GameObject player;

    private void Start()
    {
        player.GetComponent<Player>().onGameOverEvent += OnGameOver;
    }

    private void OnGameOver()
    {
        GameState = GameState.GameOver;
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        GameState = GameState.Game;
        Time.timeScale = 1;
        RoundStats.ResetGameScore();
    }
}

public enum GameState
{
    Game = 1,
    GameOver = 2,
}



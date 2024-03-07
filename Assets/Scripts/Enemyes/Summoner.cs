using System;
using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class Summoner : MonoBehaviour
{
    private static Transform pivot;

    [SerializeField]
    private Transform pivotStatCandidat;

    [SerializeField]
    private LevelManager levelManager;

    // Internal parameters

    [SerializeField]
    private EntityInfoManager[] entityInfoManagers;



    private void Start()
    {
        levelManager.onChangeGameState += OnChangeGameState;
        pivot = pivotStatCandidat;
        foreach (var entityInfoManager in entityInfoManagers)
        {
            entityInfoManager.InstantiateAll();
        }
    }

    private void OnChangeGameState(GameState gameState)
    {
        if (gameState == GameState.Game)
        {
            foreach (var entityInfoManager in entityInfoManagers)
            {
                entityInfoManager.Restart();
            }
        }
        
    }
}


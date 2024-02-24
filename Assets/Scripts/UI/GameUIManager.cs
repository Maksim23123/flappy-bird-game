using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private SerializableDict<GameState, GameObject> gamestateOverlayDict = new SerializableDict<GameState, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        levelManager.onChangeGameState += UpdateOverlay;
    }

    private void UpdateOverlay(GameState gameState)
    {
        if (gamestateOverlayDict.ContainsKey(gameState))
        {
            foreach (var value in gamestateOverlayDict.Values)
            {
                value.SetActive(false);
            }
            gamestateOverlayDict[gameState].SetActive(true);
            ILayout layout = gamestateOverlayDict[gameState].GetComponent<ILayout>();
            if (layout != null)
            {
                layout.OnWakeUp();
            }
        }
    }

}





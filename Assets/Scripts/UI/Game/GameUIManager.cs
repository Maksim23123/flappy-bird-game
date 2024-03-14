using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private SerializableDict<GameState, GameObject> gamestateOverlayDict = new SerializableDict<GameState, GameObject>();

    public static Collider particlesColector;

    [SerializeField]
    private Collider particlesColectorObject;

    // Start is called before the first frame update
    void Start()
    {
        particlesColector = particlesColectorObject;
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

    public void GoToMainMenueBtn()
    {
        levelManager.RestartGame();
        SceneManager.LoadScene(0);
    }

    

}





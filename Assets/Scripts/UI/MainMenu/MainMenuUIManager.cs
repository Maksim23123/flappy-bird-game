using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField]
    private GameData gameData;

    public GameData GameData { get => gameData; }

    public void PlayBtn()
    {
        SceneManager.LoadScene(1);
    }
}

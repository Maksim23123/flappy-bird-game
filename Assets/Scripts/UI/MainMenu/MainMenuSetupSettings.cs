using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSetupSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ResetResolution();
    }

    void SetUpResolution()
    {
        Resolution currResolution = new Resolution();
        if (PlayerPrefs.HasKey("primalScreenHight") && PlayerPrefs.HasKey("primalScreenWidth"))
        {
            currResolution.width = PlayerPrefs.GetInt("primalScreenWidth");
            currResolution.height = PlayerPrefs.GetInt("primalScreenHight");
        }
        else
        {
            currResolution = Screen.currentResolution;
            PlayerPrefs.SetInt("primalScreenWidth", currResolution.width);
            PlayerPrefs.SetInt("primalScreenHight", currResolution.height);
        }

        Screen.SetResolution(currResolution.width / 3, currResolution.height / 3, true);
    }

    void ResetResolution()
    {
        Resolution currResolution = new Resolution();
        if (PlayerPrefs.HasKey("primalScreenHight") && PlayerPrefs.HasKey("primalScreenWidth"))
        {
            currResolution.width = PlayerPrefs.GetInt("primalScreenWidth");
            currResolution.height = PlayerPrefs.GetInt("primalScreenHight");
        }
        else
        {
            currResolution = Screen.currentResolution;
            PlayerPrefs.SetInt("primalScreenWidth", currResolution.width);
            PlayerPrefs.SetInt("primalScreenHight", currResolution.height);
        }

        Screen.SetResolution(currResolution.width, currResolution.height, true);
    }
}

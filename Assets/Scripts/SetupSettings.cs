using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupSettings : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        SetUpResolution();
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
        
        Screen.SetResolution((int)(currResolution.width / 2), (int)(currResolution.height / 2), true);
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

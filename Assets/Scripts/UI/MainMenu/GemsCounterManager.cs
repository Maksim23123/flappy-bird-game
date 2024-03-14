using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemsCounterManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI;

    [SerializeField]
    private MainMenuUIManager mainMenuUIManager;

    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI.text = mainMenuUIManager.GameData.GemsCount.ToString();
    }
}

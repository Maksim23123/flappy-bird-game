using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsCounterManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI;

    [SerializeField]
    private MainMenuUIManager mainMenuUIManager;

    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI.text = mainMenuUIManager.GameData.CoinsCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RuntimeCoinCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI;

    [SerializeField]
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCount();
        levelManager.GameData.coinsCountChanged += ProcessUpdateCountRequest;
    }

    void ProcessUpdateCountRequest(object o, CurrencyEventArgs e)
    {
        UpdateCount();
    }

    
    void UpdateCount()
    {
        textMeshProUGUI.text = levelManager.GameData.CoinsCount.ToString();
    }


}

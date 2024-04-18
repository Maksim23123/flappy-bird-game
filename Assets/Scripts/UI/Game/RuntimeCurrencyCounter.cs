using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RuntimeCurrencyCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI;

    [SerializeField]
    private LevelManager levelManager;

    [SerializeField]
    private CurrencyType currencyType;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCount();
        levelManager.GameData.currencyCountChanged += ProcessUpdateCountRequest;
    }

    void ProcessUpdateCountRequest(object o, CurrencyEventArgs e)
    {
        if (e.Type == currencyType)
        {
            UpdateCount();
        }
        
    }

    
    void UpdateCount()
    {
        textMeshProUGUI.text = levelManager.GameData.GetCurrencyAmountByType(currencyType).ToString();
    }


}

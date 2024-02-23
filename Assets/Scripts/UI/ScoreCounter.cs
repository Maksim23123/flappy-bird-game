using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private string defaultText = string.Empty;
    private TextMeshProUGUI textMeshProUGUI;

    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        defaultText = textMeshProUGUI.text;
    }

    private void Update()
    {
        UpdateScoreCounter();
    }

    private void UpdateScoreCounter()
    {
        textMeshProUGUI.text = defaultText + RoundStats.GameScore;
    }


}

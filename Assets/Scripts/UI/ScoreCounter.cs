using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private readonly string defaultText = "Score: ";
    [SerializeField]
    private TextMeshProUGUI[] textMeshs;

    // Start is called before the first frame update

    private void Update()
    {
        UpdateScoreCounter();
    }

    private void UpdateScoreCounter()
    {
        foreach (var textMesh in textMeshs)
            textMesh.text = defaultText + RoundStats.GameScore;
    }


}

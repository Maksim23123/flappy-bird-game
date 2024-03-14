using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DifficultyManager : MonoBehaviour
{
    [Header("Score to Difficulty, Score(Less - more)")]
    [SerializeField]
    private SerializableDict<int, RoundStats.Difficulty> scoreDifficultyDict = new SerializableDict<int, RoundStats.Difficulty>();

    // Start is called before the first frame update
    void Start()
    {
        RoundStats.GameScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        for (int i = 0; i < scoreDifficultyDict.Keys.Count; i++)
        {
            int stageStart = scoreDifficultyDict.Keys[i];
            int stageEnd = 0;
            bool hasTopBound = false;
            if (scoreDifficultyDict.Keys.Count > i + 1)
            {
                stageEnd = scoreDifficultyDict.Keys[i + 1];
                hasTopBound = true;
            }

            if (((hasTopBound && score >= stageStart && score < stageEnd) || (!hasTopBound && score >= stageStart)) 
                && RoundStats.CurrentDificulty != scoreDifficultyDict[stageStart])
            {
                RoundStats.CurrentDificulty = scoreDifficultyDict[stageStart];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

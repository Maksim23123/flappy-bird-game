using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DropManager", menuName = "Data/DropManger")]
public class DropManager : ScriptableObject
{
    [SerializeField]
    private Tier[] tiers;

    private int probabilityRange = -1;

    void InitProbabilityRange()
    {
        probabilityRange = 0;
        foreach (var i in tiers)
        {
            probabilityRange += i.chance;
        }
    }

    public bool GetRandomDrop(out GameObject gameObject)
    {
        if (probabilityRange < 0)
        {
            InitProbabilityRange();
        }

        gameObject = null;

        int number = UnityEngine.Random.Range(0, probabilityRange);
        int progress = 0;
        bool isPlaceHolder = false;

        foreach (var i in tiers)
        {
            progress += i.chance;
            if (progress > number)
            {
                gameObject = i.drop;
                isPlaceHolder = i.isPlaceholder;
                break;
            }
        }

        if (gameObject != null && !isPlaceHolder)
            return true;
        else
            return false;
    }


    [Serializable]
    private class Tier
    {
        public int chance;

        public GameObject drop;

        public bool isPlaceholder = false;
    }
}


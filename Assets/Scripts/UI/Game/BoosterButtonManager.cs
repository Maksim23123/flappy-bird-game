using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoosterButtonManager : MonoBehaviour
{
    [SerializeField]
    GameObject clickBlocker;

    [SerializeField]
    TextMeshProUGUI count;

    [SerializeField]
    TextMeshProUGUI time;

    [SerializeField]
    Image grayCover;

    [SerializeField]
    [Range(0, 1)]
    private float grayCoverProgress = 0;

    [SerializeField]
    public BoosterCountStorage countStorage;

    public int Count
    {
        get
        {
            if (int.TryParse(count.text, out int result)) 
                return result;
            else
                return 0;
        }

        set
        {
            count.text = value.ToString();
        }
    }

    public int Time
    {
        get
        {
            if (int.TryParse(time.text, out int result))
                return result;
            else
                return 0;
        }

        set
        {
            if (value >= 0)
                time.text = value.ToString();
            else
                time.text = string.Empty;
        }
    }

    public bool IsBlocked
    {
        get
        {
            return clickBlocker.activeInHierarchy;
        }

        set
        { 
            clickBlocker.SetActive(value); 
        }
    }

    public float GrayCoverProgress { 
        get => grayCover.fillAmount; 
        set => grayCover.fillAmount = value; }

    private void Start()
    {
        GrayCoverProgress = grayCoverProgress;
        UpdateCount();
    }

    private void UpdateCount()
    {
        if (countStorage != null)
        {
            Count = countStorage.Value;
        }
        else
        {
            Count = 0;
        }
    }

    public bool UseBooster()
    {
        bool value = countStorage.Use();
        if (value)
            UpdateCount();
        return value;
    }
}

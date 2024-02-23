using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public delegate void EventHandler(GameObject sender, ChangeTimeArgs e);

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private PriorityDict<GameObject, int> priorityList;

    [SerializeField]
    private GameObject summoner;
    [SerializeField]
    private GameObject player;

    int currentPriority = 0;

    private void Start()
    {
        summoner.GetComponent<Summoner>().raise += OnEvent;
        player.GetComponent<Player>().raise += OnEvent;
    }

    private void OnEvent(GameObject sender, ChangeTimeArgs e)
    {
        int newPriority = 0;
        
        if (priorityList.ContainsKey(sender))
        {
            newPriority = priorityList[sender];
        }
        

        if (newPriority >= currentPriority)
        {
            Time.timeScale = e.TimeScale;
            currentPriority = newPriority;
        }
    }
}

public class ChangeTimeArgs : EventArgs
{
    public float TimeScale
    {
        get;
    }

    public ChangeTimeArgs(float timeScale)
    {
        TimeScale = timeScale;
    }
}

[Serializable]
public class PriorityDict<T1, T2>
{
    public List<PriorityPare<T1, T2>> priorityPares = new List<PriorityPare<T1, T2>>();
    private List<T1> keys = new List<T1>();

    public T2 this[T1 key]
    {
        get
        {
            if (InternalContainsKey(key))
            {
                return priorityPares[keys.IndexOf(key)].value;
            }
            else
            {
                throw new ArgumentException("Such key doesn't exist.", nameof(key));
            }
        }

        set
        {
            if (InternalContainsKey(key))
            {
                priorityPares[keys.IndexOf(key)].value = value;
            }
            else
            {
                Add(key, value);
            }
        }
    }

    public void Add(T1 key, T2 value)
    {
        if (!InternalContainsKey(key))
        {
            priorityPares.Add(new PriorityPare<T1, T2>(key, value));
            keys.Add(key);
        }
        else
            throw new ArgumentException("Key already exists in the dictionary.", nameof(key));
    }

    public bool ContainsKey(T1 key)
    {
        return InternalContainsKey(key);
    }

    private bool InternalContainsKey(T1 key)
    {
        SyncKeys();
        return keys.Contains(key);
    }

    private void SyncKeys()
    {
        if (priorityPares.Count != keys.Count)
        {
            keys.Clear();
            foreach (var item in priorityPares)
            {
                keys.Add(item.key);
            }
        }
    }
}

[Serializable]
public class PriorityPare<T1, T2>
{
    public T1 key;
    public T2 value;

    public PriorityPare(T1 key, T2 value)
    {
        this.value = value;
        this.key = key;
    }
}


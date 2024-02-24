using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDict<T1, T2>
{
    public List<SerializablePare<T1, T2>> serializablePares = new List<SerializablePare<T1, T2>>();
    private List<T1> keys = new List<T1>();
    private List<T2> values = new List<T2>();

    public T2 this[T1 key]
    {
        get
        {
            if (InternalContainsKey(key))
            {
                return serializablePares[keys.IndexOf(key)].value;
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
                serializablePares[keys.IndexOf(key)].value = value;
            }
            else
            {
                Add(key, value);
            }
        }
    }

    public List<T2> Values {
        get
        {
            SyncValues();
            return values;
        }
    }

    public void Add(T1 key, T2 value)
    {
        if (!InternalContainsKey(key))
        {
            serializablePares.Add(new SerializablePare<T1, T2>(key, value));
            keys.Add(key);
            values.Add(value);
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
        if (serializablePares.Count != keys.Count)
        {
            keys.Clear();
            foreach (var item in serializablePares)
            {
                keys.Add(item.key);
            }
        }
    }

    private void SyncValues()
    {
        if (serializablePares.Count != values.Count)
        {
            values.Clear();
            foreach (var item in serializablePares)
            {
                values.Add(item.value);
            }
        }
    }
}

[Serializable]
public class SerializablePare<T1, T2>
{
    public T1 key;
    public T2 value;

    public SerializablePare(T1 key, T2 value)
    {
        this.value = value;
        this.key = key;
    }
}

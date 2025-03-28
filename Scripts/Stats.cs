
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    [SerializeField] int baseValue;

    public List<int> modifiers;

    public int GetValue()
    {
        int finalValue = baseValue;

        foreach (int value in modifiers)
        {
            finalValue += value;
        }

        return finalValue;
    }

    public void BaseValue(int value) => baseValue = value;

    public void AddModifier(int _value)
    {
        modifiers.Add(_value);
    }

    public void RemoveModifier(int _value)
    {
        modifiers.Remove(_value);
    }
}

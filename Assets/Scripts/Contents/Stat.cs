using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat
{
    public StatType _statType;
    public float _baseValue;
    public float _currentValue;
    public List<StatModifier> _statModifiers;

    public Stat(StatType statType, float baseValue)
    {
        _statType = statType;
        _baseValue = _currentValue = baseValue;
        _statModifiers = new List<StatModifier>();
    }

    public void AddModifier(StatModifier statModifier)
    {
        _statModifiers.Add(statModifier);
        CalculateCurrentValue();
    }

    public void RemoveModifier(StatModifier statModifier, object source)
    {
        for (int i = _statModifiers.Count - 1; i >= 0; i--)
        {
            if (_statModifiers[i]._source == source)
            {
                _statModifiers.RemoveAt(i);
            }
        }
        CalculateCurrentValue();
    }

    public void CalculateCurrentValue()
    {
        float temp = 0;
        for (int i = 0; i < _statModifiers.Count; i++)
        {
            temp += _statModifiers[i]._modValue;
        }
        _currentValue = _baseValue + temp;
    }
}

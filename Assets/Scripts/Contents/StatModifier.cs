using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StatModifier
{
    public StatType _statType;
    public float _modValue;
    public object _source;

    public StatModifier(StatType statType, float modValue, object source)
    {
        _statType = statType;
        _modValue = modValue;
        _source = source;
    }
}
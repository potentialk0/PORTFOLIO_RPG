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

    public string StatName()
	{
        switch(_statType)
		{
            case StatType.MaxHP:
                return "체력";
                break;
            case StatType.MaxMP:
                return "마나";
                break;
            case StatType.Strength:
                return "힘";
                break;
            case StatType.Defense:
                return "방어력";
                break;
            case StatType.Magic:
                return "마력";
                break;
            case StatType.Resistance:
                return "저항";
                break;
            case StatType.MoveSpeed:
                return "이동속도";
                break;
            default:
                return "";
        }
	}

    public float StatValue()
	{
        return _modValue;
	}
}
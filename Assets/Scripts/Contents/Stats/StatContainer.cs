using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StatType
{
    MaxHP,
    MaxMP,
    Strength,
    Defense,
    Magic,
    Resistance,
    MoveSpeed,
    Num,
}

[Serializable]
public class StatContainer : MonoBehaviour
{
    [SerializeField] protected Stat[] _stats;
    [SerializeField] protected StatData _statData;
    public Stat[] Stats { get { return _stats; } }

    [SerializeField] protected float _currHP;
    [SerializeField] protected float _currMP;

    public float CurrHP { get { return _currHP; } }
    public float CurrMP { get { return _currMP; } }

    void Awake()
	{
        Init();
	}

    protected void Init()
	{
        _stats = new Stat[(int)StatType.Num];

        for(int i = 0; i < _stats.Length; i++)
		{
            _stats[i] = new Stat((StatType)i, 0);
            _stats[i]._currentValue = _stats[i]._baseValue = _statData._stats[i];
		}

        _currHP = _stats[(int)StatType.MaxHP]._baseValue;
        _currMP = _stats[(int)StatType.MaxMP]._baseValue;

	}

	#region StatChange

    public void GetDamage(StatContainer statContainer)
	{
        float damage = (statContainer._stats[(int)StatType.Strength]._currentValue
            - _stats[(int)StatType.Defense]._currentValue);
        if (damage < 0) damage = 0;

        _currHP -= damage;
	}

	#endregion
}
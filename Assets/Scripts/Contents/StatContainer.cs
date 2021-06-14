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

public class StatContainer : MonoBehaviour
{
    int _level = 1;
    int _maxExp = 0;
    int _currExp = 0;

    [SerializeField] Stat[] _stats;

    float _currHP;
    float _currMP;

    void Start()
	{
        Init();
	}

    void Init()
	{
        _stats = new Stat[(int)StatType.Num];

        for(int i = 0; i < _stats.Length; i++)
		{
            _stats[i] = new Stat((StatType)i, 0);
		}

        _stats[(int)StatType.MaxHP]     ._baseValue = 300;
        _stats[(int)StatType.MaxMP]     ._baseValue = 100;

        _stats[(int)StatType.Strength]  ._baseValue = 30;
        _stats[(int)StatType.Defense]   ._baseValue = 10;
        _stats[(int)StatType.Magic]     ._baseValue = 20;
        _stats[(int)StatType.Resistance]._baseValue = 10;

        _stats[(int)StatType.MoveSpeed] ._baseValue = 10;

        _currHP = _stats[(int)StatType.MaxHP]._baseValue;
        _currMP = _stats[(int)StatType.MaxMP]._baseValue;

        EquipmentHolder.changeEquipmentStat += OnEquip;
	}

    public void CalcStats()
	{
        for(int i = 0; i < _stats.Length; i++)
		{
            _stats[i].CalculateCurrentValue();
		}
	}

    public void OnEquip(EquipmentData equipmentData)
	{
        for (int i = 0; i < equipmentData._statModifiers.Length; i++)
        {
            _stats[(int)equipmentData._statModifiers[i]._statType].AddModifier(equipmentData._statModifiers[i]);
        }
	}
}
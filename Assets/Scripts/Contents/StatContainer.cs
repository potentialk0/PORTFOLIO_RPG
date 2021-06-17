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
    int _level = 1;
    int _maxExp = 0;
    int _currExp = 0;

    [SerializeField] Stat[] _stats;
    [SerializeField] EquipmentHolder _equipmentHolder;

    public Stat[] Stats { get { return _stats; } }

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

        for(int i = 0; i < _stats.Length; i++)
		{
            _stats[i]._currentValue = _stats[i]._baseValue;
		}

        _equipmentHolder = GameObject.Find("Player").GetComponent<EquipmentHolder>();

        EquipmentHolder.onEquip += OnEquip;
        EquipmentHolder.onUnEquip += OnUnEquip;
	}

    public void OnEquip(EquipmentData equipmentData)
	{
        if(equipmentData == null)
		{
            Debug.Log("Can't Equip : Equipment Data == null");
            return;
		}

        for (int i = 0; i < equipmentData._statModifiers.Length; i++)
        {
            _stats[(int)equipmentData._statModifiers[i]._statType].AddModifier(equipmentData._statModifiers[i]);
        }
	}

    public void OnUnEquip(EquipmentType equipmentType)
	{
        if (_equipmentHolder.GetEquipmentData(equipmentType) == null)
		{
			Debug.Log("Can't Unequip : Equipment Data == null");
			return;
		}

		for (int i = 0; i < _equipmentHolder.GetEquipmentData(equipmentType)._statModifiers.Length; i++)
		{
            _stats[(int)_equipmentHolder.GetEquipmentData(equipmentType)._statModifiers[i]._statType].
                RemoveModifier(_equipmentHolder.GetEquipmentData(equipmentType));
		}
	}
}
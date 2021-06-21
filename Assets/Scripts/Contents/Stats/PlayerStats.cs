using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerStats : StatContainer
{
    int _level = 1;
    int _maxExp = 0;
    int _currExp = 0;

    [SerializeField] EquipmentHolder _equipmentHolder;

    // Start is called before the first frame update
    void Awake()
    {
        base.Init();
        _equipmentHolder = GameObject.Find("Player").GetComponent<EquipmentHolder>();

        EquipmentHolder.onEquip += OnEquip;
        EquipmentHolder.onUnEquip += OnUnEquip;
    }

	#region Equip
	public void OnEquip(EquipmentData equipmentData)
    {
        if (equipmentData == null)
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
    #endregion
}

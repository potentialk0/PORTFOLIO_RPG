using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EquipmentType
{
    Helmet,
    Armor,
    Gloves,
    Weapon,
    Default,
}

[CreateAssetMenu(fileName = "New EquipmentData", menuName = "Scriptables/Item/Equipment", order = 0)]
public class EquipmentData : ItemData
{
    [SerializeField] public EquipmentType _equipmentType;
    [SerializeField] public StatModifier[] _statModifiers;

	private void OnEnable()
	{
        for (int i = 0; i < _statModifiers.Length; i++)
        {
            _statModifiers[i]._source = this;
        }
	}
}

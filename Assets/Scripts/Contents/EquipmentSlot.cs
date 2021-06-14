using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EquipmentSlot
{
    [SerializeField] public EquipmentData _equipmentData;
    [SerializeField] public EquipmentType _equipmentType;

    public EquipmentData EquipmentData { get { return _equipmentData; } set { _equipmentData = value; } }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EquipmentHolder : MonoBehaviour
{
    public delegate void ChangeEquipmentUI();
    public static event ChangeEquipmentUI changeEquipmentUI;

    public delegate void ChangeEquipmentStat(EquipmentData equipmentData);
    public static event ChangeEquipmentStat changeEquipmentStat;

    [SerializeField] public EquipmentSlot[] _equipmentSlots;
    [SerializeField] StatContainer _statContainer;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
	{
        _equipmentSlots = new EquipmentSlot[4];

        for(int i = 0; i < _equipmentSlots.Length; i++)
		{
            _equipmentSlots[i] = new EquipmentSlot();
            _equipmentSlots[i]._equipmentType = (EquipmentType)i;
		}

        _statContainer = gameObject.GetComponent<StatContainer>();
	}

    public void Equip(EquipmentData equipment)
	{
        for(int i = 0; i < _equipmentSlots.Length; i++)
		{
            if(_equipmentSlots[i]._equipmentType == equipment._equipmentType)
			{
                if (_equipmentSlots[i]._equipmentData == equipment) return;

                _equipmentSlots[i].EquipmentData = equipment;
                changeEquipmentUI();
                changeEquipmentStat(equipment);

                return;
			}
		}
	}
}

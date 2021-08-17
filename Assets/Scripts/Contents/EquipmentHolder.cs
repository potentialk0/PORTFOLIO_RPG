using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EquipmentHolder : MonoBehaviour
{
    public delegate void ChangeEquipmentUI();
    public static event ChangeEquipmentUI changeEquipmentUI;

    // statcontainer의 _stats[]에 전달
	public delegate void OnEquip(EquipmentData equipmentData);
	public static event OnEquip onEquip;

	public delegate void OnUnEquip(EquipmentType equipmentType);
	public static event OnUnEquip onUnEquip;

	[SerializeField] EquipmentSlot[] _equipmentSlots;
    public EquipmentSlot[] EquipmentSlots { get { return _equipmentSlots; } }

    [SerializeField] StatContainer _statContainer;

    [SerializeField] UI_Player _playerUI;

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

                if(onEquip != null)
                    onEquip(equipment);

                if (changeEquipmentUI != null)
                {
                    changeEquipmentUI();
                    _playerUI.RefreshHP();
                }

                return;
			}
		}
	}

    public void UnEquip(EquipmentType equipmentType)
	{
        if(onUnEquip != null)
            onUnEquip(equipmentType);

        _equipmentSlots[(int)equipmentType].EquipmentData = null;

        if (changeEquipmentUI != null)
        {
            changeEquipmentUI();
            _playerUI.RefreshHP();
        }
	}

    public EquipmentData GetEquipmentData(EquipmentType equipmentType)
	{
        return EquipmentSlots[(int)equipmentType].EquipmentData;
    }

    public EquipmentSlot GetEquipmentSlot(EquipmentType equipmentType)
	{
        for(int i = 0; i < _equipmentSlots.Length; i++)
		{
            if (_equipmentSlots[i]._equipmentType == equipmentType)
                return _equipmentSlots[i];
		}

        return null;
	}
}

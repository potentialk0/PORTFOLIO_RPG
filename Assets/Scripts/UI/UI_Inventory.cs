using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] GameObject _slotHolder;
    [SerializeField] UI_InventorySlot[] _slotsUI;
    [SerializeField] Inventory _inventory;

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
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        _slotHolder = GameObject.Find("SlotHolder");
        _slotsUI = _slotHolder.GetComponentsInChildren<UI_InventorySlot>();
        for(int i = 0; i < _slotsUI.Length; i++)
        {
            string n = $"InventorySlot[{i + 1}]";
            _slotsUI[i].name = n;
        }

        Inventory.onInventoryChange += RefreshUI;
    }

    // 아이템 획득 시 인벤토리(_slots) 업데이트
    public void RefreshUI()
	{
        for(int i = 0; i < _slotsUI.Length; i++)
		{
            _slotsUI[i].SetItem(_inventory.InventorySlots[i].Item);
		}
	}
}

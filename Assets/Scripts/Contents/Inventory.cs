using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnInventoryChange();
    public static event OnInventoryChange onInventoryChange;

    [SerializeField] InventorySlot[] _inventorySlots;
    public InventorySlot[] InventorySlots { get { return _inventorySlots; } }

    [SerializeField] int _slotNum;

    [SerializeField] UI_ItemShortcutSlot _itemShortcutSlot;

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
        _slotNum = 30;
        _inventorySlots = new InventorySlot[_slotNum];
        for (int i = 0; i < _inventorySlots.Length; i++)
            _inventorySlots[i] = new InventorySlot();
    }

    bool IsInventoryFull()
	{
        for(int i = 0; i < _inventorySlots.Length; i++)
		{
            if (_inventorySlots[i].Item == null) return false;
		}

        return true;
	}

    public void AddItem(ItemData item)
	{
        if (IsInventoryFull())
        {
            Debug.Log($"{gameObject.name} : Inventory is full");
            return;
        }

		for (int i = 0; i < _inventorySlots.Length; i++)
		{
            if(_inventorySlots[i].Item == item && item._isStackable == true)
			{
                _inventorySlots[i]._stackableAmount++;
                _itemShortcutSlot.SetInventorySlot(_inventorySlots[i]);
                break;
			}

			if (_inventorySlots[i]._item == null)
			{
				_inventorySlots[i]._item = item;

                if (_inventorySlots[i].Item._isStackable)
                {
                    _inventorySlots[i]._stackableAmount = 1;
                    _itemShortcutSlot.SetInventorySlot(_inventorySlots[i]);
                }
				break;
			}
		}

        if (onInventoryChange != null)
            onInventoryChange();
	}

    public void InventoryChange()
	{
        if (onInventoryChange != null)
            onInventoryChange();
    }
}

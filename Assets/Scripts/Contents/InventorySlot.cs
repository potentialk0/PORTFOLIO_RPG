using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot
{
    [SerializeField] public ItemData _item;
    [SerializeField] public bool _isStackable;
    [SerializeField] public int _stackableAmount;
    public ItemData Item { get { return _item; } set { value = _item; } }
}

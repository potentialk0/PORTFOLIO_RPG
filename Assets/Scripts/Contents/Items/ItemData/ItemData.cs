using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum ItemType
{
    Equipment,
    Stackable,
    Default,
}

[Serializable]
public abstract class ItemData : ScriptableObject
{
    static int _idNum = 0;

    public ItemType _itemType = ItemType.Default;
    public int _id;
    public string _name;
    public Sprite _itemImage;
    public GameObject _model;
    [TextArea(5, 20)] public string _description;
    public bool _isStackable;
    public int _stackedAmount = 0;
}

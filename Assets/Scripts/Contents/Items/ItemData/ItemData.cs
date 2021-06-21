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
    [SerializeField] static int _idNum = 0;

    [SerializeField] public ItemType _itemType = ItemType.Default;
    [SerializeField] public int _id;
    [SerializeField] public string _name;
    [SerializeField] public Sprite _itemImage;
    [SerializeField] public GameObject _model;
    [SerializeField] [TextArea(5, 20)] public string _description;

	private void OnEnable()
	{
        _id = _idNum++;
	}
}

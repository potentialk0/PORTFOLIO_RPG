using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemManager
{
	public Dictionary<int, ItemData> _itemData;
	bool _isInit = false;

    public void Init()
	{
		if (_isInit == false)
		{
			object[] tempLoad = Resources.LoadAll("Data/Items");

			_itemData = new Dictionary<int, ItemData>();

			for (int i = 0; i < tempLoad.Length; i++)
			{
				ItemData t = tempLoad[i] as ItemData;
				_itemData[t._id] = t;
				_itemData[t._id]._name = _itemData[t._id].name;
			}
			_isInit = true;

			if (_itemData.Count == 0) Debug.Log("Items Not Loaded Properly");
		}
	}
}

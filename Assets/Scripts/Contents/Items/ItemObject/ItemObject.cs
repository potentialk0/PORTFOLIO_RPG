using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemObject : MonoBehaviour
{
    [SerializeField] public ItemData _itemData;

    public void AddItemTo(GameObject target)
	{
		if (target == null) return;

		target.GetComponent<Inventory>().AddItem(_itemData);
	}
}

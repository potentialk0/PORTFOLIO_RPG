using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StackableData", menuName = "Scriptables/Item/Stackable", order = 1)]
public class StackableData : ItemData
{
	[TextArea(5, 20)] public string _stackableText;
	public int _healAmount;

	private void OnEnable()
	{
		_itemType = ItemType.Stackable;
		_isStackable = true;
	}
}

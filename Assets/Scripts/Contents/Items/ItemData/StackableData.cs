using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StackableData", menuName = "Scriptables/Item/Stackable", order = 1)]
public class StackableData : ItemData
{
    [SerializeField] public bool _isConsumable;
}

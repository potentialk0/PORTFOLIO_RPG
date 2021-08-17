using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDroppable : MonoBehaviour
{
    [SerializeField] ItemData[] _itemList;
    [SerializeField] float _dropChance;
    [SerializeField] ItemData _dropItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropItem()
	{
        float dropNum = Random.Range(0f, 100f);
        if(dropNum <= _dropChance)
		{
            int itemNum = Random.Range(0, _itemList.Length);
            _dropItem = _itemList[itemNum];
            Managers.Player.GetComponent<Inventory>().AddItem(_dropItem);
		}
	}
}

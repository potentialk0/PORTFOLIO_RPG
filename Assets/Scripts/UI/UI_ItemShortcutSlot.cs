using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_ItemShortcutSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] StackableData _stackableData;
    [SerializeField] int _count;
    [SerializeField] Image _itemImage;
    [SerializeField] TextMeshProUGUI _stackableAmount;
    [SerializeField] InventorySlot _inventorySlot;
    [SerializeField] UI_Player _playerUI;

	public void OnPointerClick(PointerEventData eventData)
	{
        if (_count > 0)
        {
            Managers.Player.GetComponent<StatContainer>().Heal(_stackableData._healAmount);
            _count--;
            _stackableAmount.text = _count.ToString();
            _inventorySlot._stackableAmount = _count;
            Managers.Player.GetComponent<Inventory>().InventoryChange();
            _playerUI.RefreshHP();
        }
	}

	// Start is called before the first frame update
	void Start()
    {
        _itemImage = GetComponentsInChildren<Image>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(StackableData item, int count)
	{
        _itemImage.sprite = item._itemImage;
        _itemImage.color = new Color(_itemImage.color.r, _itemImage.color.g, _itemImage.color.b, 1);
        _stackableData = item;
        _count = count;
        _stackableAmount.text = _count.ToString();
        _inventorySlot._stackableAmount = _count;
        Managers.Player.GetComponent<Inventory>().InventoryChange();
	}

    public void SetInventorySlot(InventorySlot inventorySlot)
	{
        _inventorySlot = inventorySlot;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventorySlot : MonoBehaviour
{
    [SerializeField] Image _itemImage;

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
        _itemImage = GetComponentsInChildren<Image>()[1];
	}

    public void SetItem(ItemData newItem)
	{
        if (newItem == null) return;
        _itemImage.sprite = newItem._sprite;
	}

    // 아이템이 있는 슬롯 클릭 시 아이템 정보 UI 띄우기
}

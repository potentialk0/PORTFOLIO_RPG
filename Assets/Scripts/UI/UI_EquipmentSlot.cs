using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EquipmentSlot : MonoBehaviour
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
        if (newItem == null)
            _itemImage.sprite = null;
        else
            _itemImage.sprite = newItem._sprite;
    }
}

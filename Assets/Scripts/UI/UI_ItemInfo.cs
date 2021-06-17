using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ItemInfo : MonoBehaviour
{
    [SerializeField] ItemData _item;

    [SerializeField] GameObject _itemNameUI;
    [SerializeField] GameObject _itemImageUI;
    [SerializeField] GameObject _itemDescriptionUI;

    [SerializeField] TextMeshProUGUI _itemName;
    [SerializeField] Sprite _itemImage;
    [SerializeField] TextMeshProUGUI _itemDescription;

    public string ItemName { get { return _itemName.text; } set { _itemName.text = value; } }
    public string ItemDescription { get { return _itemDescription.text; } set { _itemDescription.text = value; } }

    // Start is called before the first frame update
    void Start()
    {
        _itemName = _itemNameUI.GetComponent<TextMeshProUGUI>();
        _itemImage = _itemImageUI.GetComponent<Image>().sprite;
        _itemDescription = _itemDescriptionUI.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

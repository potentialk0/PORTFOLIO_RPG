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
    [SerializeField] GameObject _itemStat1UI;
    [SerializeField] GameObject _itemStat2UI;
    [SerializeField] GameObject _itemDescriptionUI;

    [SerializeField] TextMeshProUGUI _itemName;
    [SerializeField] Image _itemImage;
    [SerializeField] TextMeshProUGUI _itemStat1;
    [SerializeField] TextMeshProUGUI _itemStat2;
    [SerializeField] TextMeshProUGUI _itemDescription;

    public string ItemName { get { return _itemName.text; } set { _itemName.text = value; } }
    public Image ItemImage { get { return _itemImage; } set { _itemImage = value; } }
    public string ItemStat1 { get { return _itemStat1.text; } set { _itemStat1.text = value; } }
    public string ItemStat2 { get { return _itemStat2.text; } set { _itemStat2.text = value; } }
    public string ItemDescription { get { return _itemDescription.text; } set { _itemDescription.text = value; } }

    // Start is called before the first frame update
    void Awake()
    {
        _itemName = _itemNameUI.GetComponent<TextMeshProUGUI>();
        _itemImage = _itemImageUI.GetComponent<Image>();
        _itemStat1 = _itemStat1UI.GetComponent<TextMeshProUGUI>();
        _itemStat2 = _itemStat2UI.GetComponent<TextMeshProUGUI>();
        _itemDescription = _itemDescriptionUI.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

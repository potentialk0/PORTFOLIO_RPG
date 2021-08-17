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
    [SerializeField] GameObject _equipButtonUI;
    [SerializeField] GameObject _stackableTextUI;
    [SerializeField] GameObject _itemButtonUI1;
    [SerializeField] GameObject _itemButtonUI2;

    [SerializeField] TextMeshProUGUI _itemName;
    [SerializeField] Image _itemImage;
    [SerializeField] TextMeshProUGUI _itemStat1;
    [SerializeField] TextMeshProUGUI _itemStat2;
    [SerializeField] TextMeshProUGUI _itemDescription;
    [SerializeField] Button _equipButton;
    [SerializeField] TextMeshProUGUI _stackableText;
    [SerializeField] Button _itemButton1;
    [SerializeField] Button _itemButton2;

    public int _stackableCount = 0;

    public ItemData Item { get { return _item; } set { _item = value; } }
    public string ItemName { get { return _itemName.text; } set { _itemName.text = value; } }
    public Image ItemImage { get { return _itemImage; } set { _itemImage = value; } }
    public string ItemStat1 { get { return _itemStat1.text; } set { _itemStat1.text = value; } }
    public string ItemStat2 { get { return _itemStat2.text; } set { _itemStat2.text = value; } }
    public string ItemDescription { get { return _itemDescription.text; } set { _itemDescription.text = value; } }
    public Button EquipButton { get { return _equipButton; } set { _equipButton = value; } }
    public string StackableText { get { return _stackableText.text; } set { _stackableText.text = value; } }
    public Button ItemButton1 { get { return _itemButton1; } set { _itemButton1 = value; } }
    public Button ItemButton2 { get { return _itemButton2; } set { _itemButton2 = value; } }

    // Start is called before the first frame update
    void Awake()
    {
        _itemName = _itemNameUI.GetComponent<TextMeshProUGUI>();
        _itemImage = _itemImageUI.GetComponent<Image>();
        _itemStat1 = _itemStat1UI.GetComponent<TextMeshProUGUI>();
        _itemStat2 = _itemStat2UI.GetComponent<TextMeshProUGUI>();
        _itemDescription = _itemDescriptionUI.GetComponent<TextMeshProUGUI>();
        _equipButton = _equipButtonUI.GetComponent<Button>();
        _stackableText = _stackableTextUI.GetComponent<TextMeshProUGUI>();
        _itemButton1 = _itemButtonUI1.GetComponent<Button>();
        _itemButton2 = _itemButtonUI2.GetComponent<Button>();

        UI_ItemShortcutSlot shortcut1 = GameObject.Find("ItemShortcutSlot1").GetComponent<UI_ItemShortcutSlot>();
        UI_ItemShortcutSlot shortcut2 = GameObject.Find("ItemShortcutSlot2").GetComponent<UI_ItemShortcutSlot>();
        _itemButton1.onClick.AddListener(() => shortcut1.SetItem((StackableData)_item, _stackableCount));
        _itemButton2.onClick.AddListener(() => shortcut2.SetItem((StackableData)_item, _stackableCount));

        EquipmentHolder equipment = Managers.Player.GetComponent<EquipmentHolder>();
        _equipButton.onClick.AddListener(() => equipment.Equip((EquipmentData)Item));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShowEquipmentInfo()
	{
        _itemStat1UI.SetActive(true);
        _itemStat2UI.SetActive(true);
        _equipButtonUI.SetActive(true);
        _stackableTextUI.SetActive(false);
        _itemButtonUI1.SetActive(false);
        _itemButtonUI2.SetActive(false);
    }

    public void OnShowStackableInfo()
	{
        _itemStat1UI.SetActive(false);
        _itemStat2UI.SetActive(false);
        _equipButtonUI.SetActive(false);
        _stackableTextUI.SetActive(true);
        _itemButtonUI1.SetActive(true);
        _itemButtonUI2.SetActive(true);
    }

    public void OnShowEquippedInfo()
	{
        _itemStat1UI.SetActive(true);
        _itemStat2UI.SetActive(true);
        _equipButtonUI.SetActive(false);
        _stackableTextUI.SetActive(false);
        _itemButtonUI1.SetActive(false);
        _itemButtonUI2.SetActive(false);
    }

    public void DestroyItemInfoUI()
	{
        Destroy(gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ItemData _itemData;
    [SerializeField] Image _itemImage;

    [SerializeField] GameObject _itemInfo;
    [SerializeField] GameObject _equipmentInfo;

    [SerializeField] EquipmentHolder _equipmentHolder;
    [SerializeField] TextMeshProUGUI _stackableAmountUI;
    [SerializeField] int _stackableAmount = 0;

    bool _isShowingItemInfo = false;
    public bool IsShowingItemInfo { get { return _isShowingItemInfo; } set { _isShowingItemInfo = value; } }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
            CloseItemInfo();
    }

    void Init()
	{
        _itemImage = GetComponentsInChildren<Image>()[1];
        _equipmentHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<EquipmentHolder>();
	}

    public void SetItem(ItemData newItem)
	{
        if (newItem == null) return;
        _itemData = newItem;
        _itemImage.sprite = newItem._itemImage;
	}

    public void SetCount(int count)
	{
        _stackableAmount = count;
        if(_stackableAmount != 0)
            _stackableAmountUI.text = _stackableAmount.ToString();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
        ShowItemInfo();
	}

    public void ShowItemInfo()
	{
        if (_itemData == null) return;// || _isShowingItemInfo) return;

        _isShowingItemInfo = true;

        GameObject go = Resources.Load("Prefabs/UI/ItemInfoUI") as GameObject;
        _itemInfo = Instantiate(go, new Vector2(0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);

        _itemInfo.GetComponent<UI_ItemInfo>().Item = _itemData;
        _itemInfo.GetComponent<UI_ItemInfo>().ItemName = _itemData._name;
        _itemInfo.GetComponent<UI_ItemInfo>().ItemImage.sprite = _itemData._itemImage;
        _itemInfo.GetComponent<UI_ItemInfo>().ItemDescription = _itemData._description;

        if (_itemData._itemType == ItemType.Equipment)
        {
            _itemInfo.GetComponent<UI_ItemInfo>().OnShowEquipmentInfo();

            if (((EquipmentData)_itemData)._statModifiers[0].StatName() != null)
                _itemInfo.GetComponent<UI_ItemInfo>().ItemStat1 =
                    $"{((EquipmentData)_itemData)._statModifiers[0].StatName()} + {((EquipmentData)_itemData)._statModifiers[0].StatValue()}";

            if (((EquipmentData)_itemData)._statModifiers[1].StatName() != null)
                _itemInfo.GetComponent<UI_ItemInfo>().ItemStat2 =
                    $"{((EquipmentData)_itemData)._statModifiers[1].StatName()} + {((EquipmentData)_itemData)._statModifiers[1].StatValue()}";

            _itemInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 110);

            ShowEquipmentInfo();
        }
        else
		{
            _itemInfo.GetComponent<UI_ItemInfo>()._stackableCount = _stackableAmount;
            _itemInfo.GetComponent<UI_ItemInfo>().OnShowStackableInfo();
            _itemInfo.GetComponent<UI_ItemInfo>().StackableText = ((StackableData)_itemData)._stackableText;

            _itemInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 110);
        }
	}
    
    void ShowEquipmentInfo()
	{
        EquipmentData equipment = (EquipmentData)_itemData;
        EquipmentSlot equipmentSlot = _equipmentHolder.GetEquipmentSlot(equipment._equipmentType);
        EquipmentData equipmentData = equipmentSlot.EquipmentData;
        if (equipmentSlot._equipmentData == null) return;

        GameObject go = Resources.Load("Prefabs/UI/ItemInfoUI") as GameObject;
        _equipmentInfo = Instantiate(go, new Vector2(0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);

        _equipmentInfo.GetComponent<UI_ItemInfo>().OnShowEquippedInfo();
        

        _equipmentInfo.GetComponent<UI_ItemInfo>().ItemName = equipmentData._name;
        _equipmentInfo.GetComponent<UI_ItemInfo>().ItemImage.sprite = equipmentData._itemImage;
        _equipmentInfo.GetComponent<UI_ItemInfo>().ItemDescription = equipmentData._description;

        if (equipmentData._statModifiers[0].StatName() != null)
            _equipmentInfo.GetComponent<UI_ItemInfo>().ItemStat1 =
                $"{equipmentData._statModifiers[0].StatName()} + {equipmentData._statModifiers[0].StatValue()}";

        if (equipmentData._statModifiers[1].StatName() != null)
            _equipmentInfo.GetComponent<UI_ItemInfo>().ItemStat2 =
                $"{equipmentData._statModifiers[1].StatName()} + {equipmentData._statModifiers[1].StatValue()}";

        _equipmentInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -110);
    }

    public void CloseItemInfo()
	{
        _isShowingItemInfo = false;
        Destroy(_itemInfo);
        if (_equipmentInfo != null) Destroy(_equipmentInfo);
	}
}

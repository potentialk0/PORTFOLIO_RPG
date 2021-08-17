using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] GameObject _inventorySlotHolder;
    [SerializeField] UI_InventorySlot[] _inventorySlotsUI;
    [SerializeField] Inventory _inventory;

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
        for(int i = 0; i < 30; i++)
		{
            GameObject go = Instantiate(Resources.Load("Prefabs/UI/InventorySlot"), GameObject.Find("InventorySlotHolder").transform) as GameObject;
		}

        _inventorySlotHolder = GameObject.Find("InventorySlotHolder");
        _inventorySlotsUI = _inventorySlotHolder.GetComponentsInChildren<UI_InventorySlot>();
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        for(int i = 0; i < _inventorySlotsUI.Length; i++)
        {
            string n = $"InventorySlot[{i + 1}]";
            _inventorySlotsUI[i].name = n;
        }

        Inventory.onInventoryChange += RefreshUI;
    }

    // 아이템 획득 시 인벤토리(_slots) 업데이트
    public void RefreshUI()
	{
        for(int i = 0; i < _inventorySlotsUI.Length; i++)
		{
            _inventorySlotsUI[i].SetItem(_inventory.InventorySlots[i].Item);
            _inventorySlotsUI[i].SetCount(_inventory.InventorySlots[i]._stackableAmount);
		}
	}

	#region Animation

    [SerializeField] float _moveSpeed = 3000f;
    [SerializeField] bool _isEnabled = false;

	public void Enable()
	{
        if (_isEnabled) return;
        _isEnabled = true;
        StartCoroutine(EnableUI());
	}

    public void Disable()
	{
        if (!_isEnabled) return;
        _isEnabled = false;
        StartCoroutine(DisableUI());
	}

    IEnumerator EnableUI()
	{
        RectTransform rect = GetComponent<RectTransform>();

        while(rect.anchoredPosition.x >= 260)
		{
            transform.position -= transform.right * _moveSpeed * Time.deltaTime;
            yield return null;
		}

        if (rect.anchoredPosition.x < 260)
            rect.anchoredPosition = new Vector2(260, rect.anchoredPosition.y);
	}

    IEnumerator DisableUI()
	{
        RectTransform rect = GetComponent<RectTransform>();

        while(rect.anchoredPosition.x <= 540)
		{
            transform.position += transform.right * _moveSpeed * Time.deltaTime;
            yield return null;
		}

        if (rect.anchoredPosition.x > 540)
            rect.anchoredPosition = new Vector2(540, rect.anchoredPosition.y);
    }
	#endregion
}

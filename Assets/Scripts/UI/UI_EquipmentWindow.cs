using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_EquipmentWindow : MonoBehaviour
{
    [SerializeField] GameObject _equipmentSlotHolder;
    [SerializeField] UI_EquipmentSlot[] _equipmentSlotsUI;
    [SerializeField] EquipmentHolder _equipmentHolder;

    [SerializeField] StatContainer _statContainer;
    [SerializeField] TextMeshProUGUI[] _statsUI;

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
        _equipmentSlotHolder = GameObject.Find("EquipmentSlotHolder");
        _equipmentSlotsUI = _equipmentSlotHolder.GetComponentsInChildren<UI_EquipmentSlot>();
        _equipmentHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<EquipmentHolder>();

        _statContainer = GameObject.FindGameObjectWithTag("Player").GetComponent<StatContainer>();

        RefreshStatsUI();

        EquipmentHolder.changeEquipmentUI += RefreshUI;
    }

    public void RefreshUI()
    {
        RefreshEquipmentSlotUI();
        RefreshStatsUI();
    }

    void RefreshEquipmentSlotUI()
	{
        for (int i = 0; i < _equipmentSlotsUI.Length; i++)
        {
            _equipmentSlotsUI[i].SetItem(_equipmentHolder.EquipmentSlots[i]._equipmentData);
        }
    }

    void RefreshStatsUI()
	{
        for (int i = 0; i < _statsUI.Length; i++)
        {
            _statsUI[i].text = _statContainer.Stats[i + 2]._currentValue.ToString();
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

        while (rect.anchoredPosition.x <= -300)
        {
            transform.position += transform.right * _moveSpeed * Time.deltaTime;
            yield return null;
        }

        if (rect.anchoredPosition.x > -300)
            rect.anchoredPosition = new Vector2(-300, rect.anchoredPosition.y);
    }

    IEnumerator DisableUI()
    {
        RectTransform rect = GetComponent<RectTransform>();

        while (rect.anchoredPosition.x > -500)
        {
            transform.position -= transform.right * _moveSpeed * Time.deltaTime;
            yield return null;
        }

        if (rect.anchoredPosition.x < -500)
            rect.anchoredPosition = new Vector2(-500, rect.anchoredPosition.y);
    }

	#endregion
}

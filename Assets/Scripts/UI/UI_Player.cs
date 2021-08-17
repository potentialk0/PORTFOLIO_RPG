using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Player : MonoBehaviour
{
    [SerializeField] Slider _hpBar;
    [SerializeField] TextMeshProUGUI _hpText;
    [SerializeField] StatContainer _playerStats;

    // Start is called before the first frame update
    void Start()
    {
        _hpBar.value = _playerStats.CurrHP / _playerStats.Stats[(int)StatType.MaxHP]._currentValue;
        _hpText.text = $"{_playerStats.CurrHP} / {_playerStats.Stats[(int)StatType.MaxHP]._currentValue}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshHP()
	{
        _hpBar.value = _playerStats.CurrHP / _playerStats.Stats[(int)StatType.MaxHP]._currentValue;
        _hpText.text = $"{_playerStats.CurrHP} / {_playerStats.Stats[(int)StatType.MaxHP]._currentValue}";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_QuestWindow : MonoBehaviour
{
    [SerializeField] UI_QuestList _questListUI;
    [SerializeField] UI_QuestInfo _questInfoUI;

    public UI_QuestList QuestListUI { get { return _questListUI; } }
    public UI_QuestInfo QuestInfoUI { get { return _questInfoUI; } }

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
        NPC.onShowQuest += Enable;
    }

    void ShowQuestList()
	{
        _questListUI.gameObject.SetActive(true);
        _questInfoUI.gameObject.SetActive(false);
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

        while (rect.anchoredPosition.x >= 260)
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

        while (rect.anchoredPosition.x <= 540)
        {
            transform.position += transform.right * _moveSpeed * Time.deltaTime;
            yield return null;
        }

        if (rect.anchoredPosition.x > 540)
            rect.anchoredPosition = new Vector2(540, rect.anchoredPosition.y);
    }
    #endregion
}

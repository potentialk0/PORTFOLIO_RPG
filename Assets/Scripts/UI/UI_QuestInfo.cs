using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_QuestInfo : MonoBehaviour
{
    [SerializeField] QuestData _questData;

    [SerializeField] TextMeshProUGUI _questTitle;
    [SerializeField] TextMeshProUGUI _questScript;
    [SerializeField] TextMeshProUGUI _questDescription;
    [SerializeField] Image[] _rewardImage;
    [SerializeField] Button _acceptButton;
    [SerializeField] Button _rejectButton;
    [SerializeField] TextMeshProUGUI _questStatus;

    // Start is called before the first frame update
    void Start()
    {
        NPC.onInitQuestUI += SetQuest;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetQuest(QuestData questData)
	{
        _questData = questData;
        _questTitle.text = _questData._questTitle;
        _questScript.text = _questData._questScript;
        _questDescription.text = _questData._questDescription;

        if (questData._rewardItems.Length != 0)
        {
            for (int i = 0; i < _questData._rewardItems.Length; i++)
            {
                _rewardImage[i].sprite = _questData._rewardItems[i]._itemImage;
            }
        }
	}

    public void RefreshQuestStatus(int currentcount, int totalCount)
	{
        _questStatus.text = $"현재 처치 : {currentcount} / {totalCount}";
	}

    public void HideButtons()
	{
        _acceptButton.gameObject.SetActive(false);
        _rejectButton.gameObject.SetActive(false);
        _questStatus.gameObject.SetActive(true);
    }

    public void ShowButtons()
	{
        _acceptButton.gameObject.SetActive(true);
        _rejectButton.gameObject.SetActive(true);
        _questStatus.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    private static Quests _instance;
    public static Quests Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(Quests)) as Quests;

                if (_instance == null)
                {
                    Debug.Log("Quests Singleton Missing");
                }
            }
            return _instance;
        }
    }

    public delegate void OnEnableQuest(int questIndex);
    public static event OnEnableQuest onEnableQuest;

    public delegate void OnCompleteQuest(int questIndex);
    public static event OnCompleteQuest onCompleteQuest;

    [SerializeField] List<QuestData> _questList;
    public List<QuestData> QuestList { get { return _questList; } }

    UI_QuestWindow QuestUI;

    [SerializeField] ItemData _potion;

    // Start is called before the first frame update
    void Start()
    {
        QuestUI = GameObject.Find("QuestUI").GetComponent<UI_QuestWindow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            ActivateQuest(0);
    }

    public void ActivateQuest(int questIndex)
    {
        if (QuestList[questIndex]._isActive == false && QuestList[questIndex]._isComplete == false)
        {
            if (onEnableQuest != null)
                onEnableQuest(questIndex);

            QuestList[questIndex]._isActive = true;
            for (int i = 0; i < 20; i++)
            {
                Managers.Player.GetComponent<Inventory>().AddItem(_potion);
            }
        }
    }

    public void CompleteQuest(int questIndex)
    {
        if (QuestList[questIndex]._isComplete == false)
        {
            if (onCompleteQuest != null)
                onCompleteQuest(questIndex);

            QuestList[questIndex]._isActive = false;
            QuestList[questIndex]._isComplete = true;
        }
    }

    public void AddCount(int questIndex)
	{
        QuestList[questIndex]._questCurrCount++;
        QuestUI.QuestInfoUI.RefreshQuestStatus(QuestList[questIndex]._questCurrCount, QuestList[questIndex]._questTotalCount);
        if (QuestList[questIndex]._questCurrCount >= QuestList[questIndex]._questTotalCount)
            CompleteQuest(questIndex);
	}
}

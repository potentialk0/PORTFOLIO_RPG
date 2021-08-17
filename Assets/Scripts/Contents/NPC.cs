using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public delegate void OnInitQuestUI(QuestData questData);
    public static event OnInitQuestUI onInitQuestUI;

    public delegate void OnShowQuest();
    public static event OnShowQuest onShowQuest;

    [SerializeField] QuestData _currentQuest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateClick();
    }

    bool IsCloseEnough()
	{
        if (Vector3.Distance(transform.position, Managers.Player.transform.position) < 10f)
            return true;
        return false;
	}

    void UpdateClick()
	{
        if (Input.GetMouseButton(0) && IsCloseEnough())
        {
            if (Utils.IsUIHit())
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    if (_currentQuest._isComplete == false)
                    {
                        if (_currentQuest._isActive == false)
                        {
                            onInitQuestUI(_currentQuest);
                            onShowQuest();
                        }
                    }
                    else
					{
                        if(!_currentQuest._isRewarded)
						{
                            for (int i = 0; i < _currentQuest._rewardItems.Length; i++)
                            {
                                Managers.Player.GetComponent<Inventory>().AddItem(_currentQuest._rewardItems[i]);
                            }
                            _currentQuest._isRewarded = true;
						}
					}
                    Debug.Log("NPC Clicked");
                }
            }
        }
    }
}

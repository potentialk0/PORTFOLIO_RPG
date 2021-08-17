using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCountable : MonoBehaviour
{
    [SerializeField] bool _isCounting = false;
	[SerializeField] int _questIndex;

	private void Start()
	{
		
	}

	public void Init()
	{
		Quests.onEnableQuest += StartCounting;
		Quests.onCompleteQuest += EndCounting;
	}

	// onEnableQuest
	void StartCounting(int questIndex)
	{
		if(_questIndex == questIndex)
			_isCounting = true;
	}

	// onCompleteQuest
	void EndCounting(int questIndex)
	{
		if (_questIndex == questIndex)
			_isCounting = false;
	}

	// onDeath
	public void AddQuestCount()
	{
		if(_isCounting)
			Quests.Instance.AddCount(_questIndex);
	}
}

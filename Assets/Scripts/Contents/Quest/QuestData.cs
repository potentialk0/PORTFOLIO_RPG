using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuestData", menuName = "Scriptables/QuestData", order = 0)]
public class QuestData : ScriptableObject
{
    public int _questID;
    public bool _isActive = false;
    public bool _isComplete = false;
    public bool _isRewarded = false;

    public int _questCurrCount = 0;
    public int _questTotalCount;

    public string _questTitle;
    [TextArea(5, 20)] public string _questScript;
    public string _questDescription;

    public int _rewardGold;
    public int _rewardExp;
    public ItemData[] _rewardItems;

	private void OnEnable()
	{
        _isActive = false;
        _questCurrCount = 0;
	}
}

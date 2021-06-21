using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StatData", menuName = "Scriptables/StatData", order = 0)]
public class StatData : ScriptableObject
{
    [SerializeField] public float[] _stats;
    [SerializeField] string _comment;

	public StatData()
	{
		_stats = new float[(int)StatType.Num];
		_comment = "스텟 순서 : 체 마 공 방 마 저 이속";
	}
}

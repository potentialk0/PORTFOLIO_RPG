using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New SkillData", menuName = "Scriptables/Skill", order = 0)]
public class SkillData : ScriptableObject
{
    [SerializeField] public string _skillName;
    [SerializeField] public Sprite _skillImage;
    [SerializeField] public AnimationClip _animationClip;
    [SerializeField] public float _damage;
    [SerializeField] public float _cooltime;
    [SerializeField] [TextArea(10, 5)] public string _skillDescription;

    float _timer;
    bool _isCooltime;
    public bool IsCooltime { get { return _isCooltime; } }

	private void OnEnable()
	{
        _timer = 0;
        _isCooltime = false;
	}

	public IEnumerator StartCooltime()
	{
        _isCooltime = true;
        while(_timer < _cooltime)
		{
            yield return null;
            _timer += Time.deltaTime;
            if(_timer >= _cooltime)
			{
                _isCooltime = false;
                _timer = 0;
                break;
			}
		}
	}

    public void InitCooltime()
	{
        _timer = 0;
	}
}

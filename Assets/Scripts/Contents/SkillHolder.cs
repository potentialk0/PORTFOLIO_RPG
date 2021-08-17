using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    public delegate void OnSkillChange();
    public static event OnSkillChange onSkillChange;

    [SerializeField] SkillData[] _skillData;
    [SerializeField] SkillData[] _shortcutSkills; // 숏컷에 등록 된 스킬

    [SerializeField] SkillData _currentSkill;
    [SerializeField] float _animTimer = 0;
    [SerializeField] float _currentAnimLength = 0;
    [SerializeField] bool _isAnimPlaying = false;
    public bool IsAnimPlaying { get { return _isAnimPlaying; } }

    Animator _animator; // 임시

    // Start is called before the first frame update
    void Start()
    {
        object[] tempLoad = Resources.LoadAll("Data/Skills");
        _skillData = new SkillData[tempLoad.Length];

        for(int i = 0; i < tempLoad.Length; i++)
		{
            _skillData[i] = tempLoad[i] as SkillData;
		}

        _animator = GetComponent<Animator>();
        _currentSkill = _shortcutSkills[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AutoMode()
	{
        if (_isAnimPlaying == false)
        {
            SetCurrentSkill();
            StartCoroutine(StartAnimationTimer());
            UseSkill();
        }
	}

    public void SetCurrentSkill()
	{
        for(int i = 0; i < _shortcutSkills.Length; i++)
		{
            if (_shortcutSkills[i].IsCooltime == false)
            {
                _currentSkill = _shortcutSkills[i];
                StartCoroutine(_currentSkill.StartCooltime());
                _currentAnimLength = GetCurrentAnimLength();
                break;
            }
		}
	}

    void UseSkill()
	{
        _animator.CrossFade(_currentSkill._skillName, 0.2f);
        // 데미지
	}

    IEnumerator StartAnimationTimer()
    {
        _isAnimPlaying = true;
        while (_animTimer < _currentAnimLength)
        {
            yield return null;
            _animTimer += Time.deltaTime;
            if (_animTimer >= _currentAnimLength)
            {
                _isAnimPlaying = false;
                _animTimer = 0;
                break;
            }
        }
    }

    float GetCurrentAnimLength()
	{
        for(int i = 0; i < _animator.runtimeAnimatorController.animationClips.Length; i++)
		{
            if (_animator.runtimeAnimatorController.animationClips[i].name == _currentSkill._skillName)
                return _animator.runtimeAnimatorController.animationClips[i].length;
		}
        return 0;
	}
}

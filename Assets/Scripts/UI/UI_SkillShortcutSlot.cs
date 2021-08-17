using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillShortcutSlot : MonoBehaviour
{
    [SerializeField] SkillData _skillData;
    [SerializeField] Image _skillImage;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        if (_skillData != null)
        {
            _skillImage.sprite = _skillData._skillImage;
            _skillImage.color = new Color(_skillImage.color.r, _skillImage.color.g, _skillImage.color.b, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        _skillImage = GetComponentsInChildren<Image>()[1];
    }

    public void SetSkill(SkillData newSkill)
    {
        if (newSkill == null)
            _skillImage.sprite = null;
        else
            _skillImage.sprite = newSkill._skillImage;
    }
}

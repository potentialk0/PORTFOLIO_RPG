using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillShortcut : MonoBehaviour
{
    [SerializeField] UI_SkillShortcutSlot[] _skillShortcutSlots;

    // Start is called before the first frame update
    void Start()
    {
        _skillShortcutSlots = GetComponentsInChildren<UI_SkillShortcutSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

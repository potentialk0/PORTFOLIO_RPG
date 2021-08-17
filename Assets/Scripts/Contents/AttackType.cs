using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackType : MonoBehaviour
{
    GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnNormalAttack()
	{
        _target = GetComponent<PlayerController>()._target;
        _target.GetComponent<StatContainer>().GetDamageFrom(GetComponent<StatContainer>());
        _target.GetComponent<MonsterAI>().OnGetHit();
	}

    void OnKnockBackAttack()
    {
        _target = GetComponent<PlayerController>()._target;
        _target.GetComponent<StatContainer>().GetDamageFrom(GetComponent<StatContainer>());
        _target.GetComponent<MonsterAI>().OnKnockBack();
    }
}

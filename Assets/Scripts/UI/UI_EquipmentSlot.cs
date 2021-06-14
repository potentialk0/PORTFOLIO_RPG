using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EquipmentSlot : MonoBehaviour
{
    EquipmentHolder _equipmentHolder;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
	{
        _equipmentHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<EquipmentHolder>();
        EquipmentHolder.changeEquipmentUI += RefreshUI;
    }

    void RefreshUI()
	{

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // Canvas/TestImage에 붙어있음
    [SerializeField]
    Sprite[] aurora;

    Image I;
    Inventory _inventory;
    EquipmentHolder _equipmentHolder;
    [SerializeField] ItemData _testItem;

    // Start is called before the first frame update
    void Start()
    {
        I = GetComponent<Image>();
        I.sprite = aurora[0];
        StartCoroutine(Size());

        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        _equipmentHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<EquipmentHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
		{
            _inventory.AddItem(_testItem);
		}

        if(Input.GetKeyDown(KeyCode.Alpha1))
		{
            _equipmentHolder.Equip((EquipmentData)_testItem);
		}

        if(Input.GetKeyDown(KeyCode.Alpha3))
		{
            _equipmentHolder.UnEquip(EquipmentType.Helmet);
		}
    }

    IEnumerator Repeat()
	{
        while(true)
		{
            I.sprite = aurora[0];
            yield return new WaitForSeconds(0.05f);
            I.sprite = aurora[1];
            yield return new WaitForSeconds(0.05f);
        }
	}

    float t = 0;
    float _scaleSpeed = 15f;
    float _scaleRange = 0.1f;
    IEnumerator Size()
	{
        
        while(true)
		{
            t += Time.deltaTime;
            float offset = Mathf.Sin(t * _scaleSpeed) * _scaleRange;
            transform.localScale = new Vector3(1 + offset, 1 + offset, 1 + offset);
            yield return null;
        }
	}
}

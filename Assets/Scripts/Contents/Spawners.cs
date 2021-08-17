using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    private static Spawners _instance;
    public static Spawners Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(Spawners)) as Spawners;

                if (_instance == null)
                {
                    Debug.Log("Spawners Singleton Missing");
                }
            }
            return _instance;
        }
    }

    [SerializeField] List<Spawner> _spawners;
    public List<Spawner> SpawnerList { get { return _spawners; } }

    GameObject _closestMonster;

    // Start is called before the first frame update
    void Start()
    {
        Spawner[] spawners = GetComponentsInChildren<Spawner>();
        for(int i = 0; i < spawners.Length; i++)
		{
            _spawners.Add(spawners[i]);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetClosestMonster()
	{
        _closestMonster = null;
        for(int i = 0; i < _spawners.Count; i++)
		{
            if(_spawners[i].GetClosestMonster() != null)// &&
                //_spawners[i].GetClosestMonster().GetComponent<MonsterAI>().State != MonsterState.Dead)
			{
                _closestMonster = _spawners[i].GetClosestMonster();
			}
		}

        if (_closestMonster == null)
            Debug.Log("Spawner Closest Monster Null");
        return _closestMonster;
	}
}

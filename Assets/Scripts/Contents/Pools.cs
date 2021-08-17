using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolType
{
    SlimePool,
    TargetMark,
}

public class Pools : MonoBehaviour
{
    private static Pools _instance;
    public static Pools Instance
	{
		get
		{
            if(_instance == null)
			{
                _instance = FindObjectOfType(typeof(Pools)) as Pools;

                if(_instance == null)
				{
                    Debug.Log("Pools Singleton Missing");
				}
			}
            return _instance;
		}
	}

    [SerializeField] GameObject _poolPrefab;
    [SerializeField] Dictionary<PoolType, Pool> _pools;
    public Dictionary<PoolType, Pool> PoolDict { get { return _pools; } }

    [SerializeField] GameObject _slime;
    [SerializeField] GameObject _targetMark;

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
        _pools = new Dictionary<PoolType, Pool>();
        _poolPrefab = Resources.Load("Prefabs/Pool") as GameObject;
        MakePool(PoolType.SlimePool, _slime, 50);
        MakePool(PoolType.TargetMark, _targetMark, 3);
	}

    void MakePool(PoolType poolType, GameObject poolObject, int poolCount)
	{
        GameObject go = Instantiate(_poolPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<Pool>().Init(poolType, poolObject, poolCount);
        _pools.Add(poolType, go.GetComponent<Pool>());
	}
}

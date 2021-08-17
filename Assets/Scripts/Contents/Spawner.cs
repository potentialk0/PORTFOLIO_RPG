using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected GameObject _monsterPrefab;
    [SerializeField] protected List<GameObject> _monsterList;
    public List<GameObject> MonsterList { get { return _monsterList; } }

    [SerializeField] protected int              _maxNum;
    [SerializeField] protected float            _spawnCooltime;
    [SerializeField] protected float            _spawnRange;
    [SerializeField] protected Vector3          _spawnPoint;

    [SerializeField] protected float _spawnTimer = 0;
    protected Transform _playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        Init("Slime", 10, 1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        Spawn(PoolType.SlimePool);
    }

    protected void Init(string monsterName, int maxNum, float spawnCooltime, float spawnRange)
	{
        _monsterList = new List<GameObject>();
        _spawnPoint = gameObject.transform.position;
        _monsterPrefab = Resources.Load($"Prefabs/Monsters/{monsterName}") as GameObject;
        _maxNum = maxNum;
        _spawnCooltime = spawnCooltime;
        _spawnRange = spawnRange;
        _playerTransform = GameObject.Find("Player").transform;
	}

    protected void SetRandomPosition()
	{
        float ranX = Random.Range(-1f, 1f) * _spawnRange;
        float ranZ = Random.Range(-1f, 1f) * _spawnRange;

        _spawnPoint = transform.position + new Vector3(ranX, 0, ranZ);
    }

    protected void Spawn(PoolType poolType)
	{
        if(_monsterList.Count < _maxNum)
		{
            _spawnTimer += Time.deltaTime;
            if(_spawnTimer >= _spawnCooltime)
			{
                SetRandomPosition();
                GameObject go = Pools.Instance.PoolDict[poolType].Dequeue(_spawnPoint);
                _monsterList.Add(go);
                go.GetComponent<MonsterAI>().SetSpawner(gameObject.GetComponent<Spawner>());
                go.GetComponent<MonsterAI>().SetPooltype(poolType);
                _spawnTimer = 0;
			}
		}
	}

    public void RemoveObject(GameObject go)
	{
        _monsterList.Remove(go);
	}

    public GameObject GetClosestMonster()
	{
        GameObject closestMonster = _monsterList[0];
        for(int i = 0; i < _monsterList.Count - 1; i++)
		{
            if (_monsterList[i].GetComponent<MonsterAI>().State != MonsterState.Dead &&
                Vector3.Distance(_monsterList[i].transform.position, _playerTransform.position) < 10)
            {
                closestMonster = _monsterList[i];
                break;
            }
            else
            {
                if (_monsterList[i].GetComponent<MonsterAI>().State != MonsterState.Dead && 
                    Vector3.Distance(_monsterList[i].transform.position, _playerTransform.position) >
                    Vector3.Distance(_monsterList[i + 1].transform.position, _playerTransform.position))
                {
                    closestMonster = _monsterList[i + 1];
                }
            }
		}
        if (closestMonster == null)
            Debug.Log("Spawner : Couldn't Get Closest Monster");
        return closestMonster;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] GameObject _poolObject;
    [SerializeField] int _poolCount = 0;

    [SerializeField] Queue<GameObject> _pool;
    [SerializeField] PoolType _poolType;
    public Queue<GameObject> PoolQueue { get { return _pool; } }
    public PoolType PoolType { get { return _poolType; } }

	public void Init(PoolType poolType, GameObject prefab, int poolCount)
	{
        _pool = new Queue<GameObject>();
        _poolObject = prefab;
        name = poolType.ToString();
        for(int i = 0; i < poolCount; i++)
		{
            InstantiateGO();
		}
    }

    void InstantiateGO()
	{
        GameObject go = Instantiate(_poolObject, transform.position, Quaternion.identity, transform);
        go.name = $"{go.name} {_poolCount++}";
        if (go.GetComponent<QuestCountable>() != null)
            go.GetComponent<QuestCountable>().Init();
        go.SetActive(false);
        _pool.Enqueue(go);
    }

    public void Enqueue(GameObject gameObject)
	{
        _pool.Enqueue(gameObject);
        gameObject.SetActive(false);
	}

    public void Enqueue(GameObject gameObject, bool useParent = true)
    {
        _pool.Enqueue(gameObject);
        if (useParent == true)
        {
            gameObject.transform.SetParent(this.transform);
        }

        gameObject.SetActive(false);
    }

    public GameObject Dequeue(Vector3 position)
	{
        GameObject go;
        if (_pool.Count > 0)
        {
            go = _pool.Dequeue();
        }
        else
		{
            InstantiateGO();
            go = _pool.Dequeue();
        }
        go.SetActive(true);
        go.transform.position = position;
        return go;
	}

    public GameObject Dequeue(Vector3 position, Transform parent)
    {
        GameObject go;
        if (_pool.Count > 0)
        {
            go = _pool.Dequeue();
        }
        else
        {
            InstantiateGO();
            go = _pool.Dequeue();
        }
        go.SetActive(true);
        go.transform.position = position;
        go.transform.parent = parent;
        return go;
    }
}
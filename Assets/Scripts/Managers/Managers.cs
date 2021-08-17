using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers _instance;
    public static Managers Instance { get { Init(); return _instance; } }

    PlayerController _player;
    ItemManager _item = new ItemManager();

    public static PlayerController Player { get { return Instance._player; } }
    public static ItemManager Item { get { return Instance._item; } }

    void Awake()
	{
        Init();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void Init()
    {
        GameObject go = GameObject.Find("@Managers");
        if (go == null)
        {
            go = new GameObject { name = "@Managers" };
            go.AddComponent<Managers>();
        }

        DontDestroyOnLoad(go);

        _instance = go.GetComponent<Managers>();

        _instance._item.Init();
    }
}

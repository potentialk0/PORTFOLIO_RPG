using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSettings : MonoBehaviour
{
    [SerializeField] Texture2D _cursor;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(_cursor, Vector3.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

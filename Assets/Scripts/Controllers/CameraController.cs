using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject _player = null;
    [SerializeField] Vector3 _delta = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_player == null) return;

        transform.position = _player.transform.position + _delta;
        transform.LookAt(_player.transform.position);
    }

    public void Shake()
	{

	}
}

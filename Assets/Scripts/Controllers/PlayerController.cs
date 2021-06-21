using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // 애니메이션
    Animator _animator;

    // 이동 지점 설정
    NavMeshPath _path;
    Vector3 _destination;
    Vector3 _targetPos;

    // 이동 구현
    Vector3[] _corners;
    int _cornerNum = 0;
    float _arriveOffset = 0.2f;

    float _rotSpeed = 10f;
    float _moveSpeed = 7f;

    // 자동 전투
    bool _isAutoMode = false;
    public GameObject _target;
    float _attackRange = 0.5f;

    public enum PlayerState
    {
        Idle,
        Move,
        Combat,
        Dead,
    }

    PlayerState _state = PlayerState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _path = new NavMeshPath();
        
        _destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateClick();
        ProcessState();
    }
    void ProcessState()
    {
        //Debug.Log(_state);
        switch (_state)
        {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Move:
                UpdateMove();
                break;
            case PlayerState.Combat:
                UpdateCombat();
                break;
            case PlayerState.Dead:
                UpdateDead();
                break;
        }
    }

    public void ChangeState(PlayerState s)
    {
        if (_state == s) return;
        switch (s)
        {
            case PlayerState.Idle:
                _animator.CrossFade("IDLE", 0.2f);
                _state = PlayerState.Idle;
                break;
            case PlayerState.Move:
                _animator.CrossFade("RUN", 0.1f);
                _state = PlayerState.Move;
                break;
            case PlayerState.Combat:
                _state = PlayerState.Combat;
                break;
            case PlayerState.Dead:
                _state = PlayerState.Dead;
                break;
        }
    }

	#region UpdateIdle
	void UpdateIdle()
    {

    }
	#endregion

	#region UpdateMove
	void UpdateMove()
    {
        if (HasArrivedTo(_destination))
        {
            ChangeState(PlayerState.Idle);
            return;
        }
        Move();
    }
	#endregion


	#region UpdateCombat
	[SerializeField]
    GameObject[] _testEnemy;
    int _testNum = 0;

    void UpdateCombat()
    {
        if (_target == null)
        {
            if (_isAutoMode)
            {
                SetNewTarget();
                return;
            }
            Debug.Log("Combat Target is NULL");
            ChangeState(PlayerState.Idle);
            return;
        }

        if (IsTargetInRange(_target))
        {
            Attack();
        }
        else
        {
            MoveToTarget();
        }
    }

    void SetNewTarget()
    {
        if(_testEnemy[_testNum + 1] != null)
            _target = _testEnemy[++_testNum];
    }

    bool IsTargetInRange(GameObject target)
    {
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
            new Vector2(target.transform.position.x, target.transform.position.z)) <= _attackRange)
            return true;
        return false;
    }

    void Attack()
    {
        // 쿨타임 따라 스킬
        Debug.Log("Attack Confirmed!");
    }

    void MoveToTarget()
	{
        if(_target == null)
		{
            Debug.Log("MoveToTarget _target null");
            return;
		}

        SetPath(_target.transform.position);
        _animator.CrossFade("RUN", 0.1f);
        Move();
	}
	#endregion

	#region UpdateDead
	void UpdateDead()
    {

    }
	#endregion

	string ClickedTag()
	{
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.tag;
        }

        return hit.transform.tag;
	}

    void UpdateClick()
	{
        if (Input.GetMouseButton(0))
        {
            if (Utils.IsUIHit())
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Ground")
                {
                    SetPath(hit.point);
                    ChangeState(PlayerState.Move);
                    return;
                }
                else if (hit.transform.tag == "Enemy")
                {
                    Debug.Log("Enemy Clicked");
                    SetTarget(hit.transform.gameObject);
                    ChangeState(PlayerState.Combat);
                    return;
                }
            }
        }
	}

    void SetTarget(GameObject target)
	{
        _target = target;
	}

    void SetPath(Vector3 destination)
	{
        _cornerNum = 1;
        _destination = new Vector3(destination.x, transform.position.y, destination.z);
        if (NavMesh.CalculatePath(transform.position, _destination, NavMesh.AllAreas, _path))
        {
            _corners = _path.corners;
            return;
        }
    }

	void Move()
	{
        if (_corners != null && _corners.Length > 0)
        {
            _targetPos = _corners[_cornerNum];
            LookAtTarget(_targetPos);
            //if (HasArrivedTo(destination) && _currentState != STATE.ATTACK)
            //{
            //    StayStill();
            //    ChangeState(STATE.IDLE);
            //    return;
            //}
            if (!HasArrivedTo(_destination) && HasArrivedTo(_corners[_cornerNum]))
                _cornerNum++;
            else if (!HasArrivedTo(_destination))
                transform.position += (_corners[_cornerNum] - transform.position).normalized * Time.deltaTime * _moveSpeed;
        }
    }

    void LookAtTarget(Vector3 target)
    {
        if (target != transform.position)
        {
            Quaternion targetRot = Quaternion.LookRotation(target - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * _rotSpeed);
        }
    }

    bool HasArrivedTo(Vector3 pos)
    {
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(pos.x, pos.z)) <= _arriveOffset)
            return true;
        return false;
    }
}

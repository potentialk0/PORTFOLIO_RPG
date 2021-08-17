using UnityEngine;
using UnityEngine.AI;

public enum PlayerState
{
    Idle,
    Move,
    SetTarget,
    Chase,
    Attack,
    Dead,
    Num,
}

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
    int _cornerNum = 1;
    float _arriveOffset = 0.2f;

    float _rotSpeed = 10f;
    float _moveSpeed = 7f;

    // 자동 전투
    [SerializeField] bool _isAutoMode = true;
    public GameObject _target;
    MonsterAI _targetAI;
    float _attackRange = 1f;

    [SerializeField] PlayerState _state = PlayerState.Idle;

    [SerializeField] AnimationClip[] _animationClips;
    [SerializeField] SkillHolder _skillHolder;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _path = new NavMeshPath();
        
        _destination = transform.position;

        _animationClips = _animator.runtimeAnimatorController.animationClips;

        _skillHolder = GetComponent<SkillHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateClick();
        ProcessState();
    }

    public void AutoMode()
	{
        ChangeState(PlayerState.SetTarget);
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
            case PlayerState.SetTarget:
                UpdateSetTarget();
                break;
            case PlayerState.Chase:
                UpdateChase();
                break;
            case PlayerState.Attack:
                UpdateAttack();
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
            case PlayerState.SetTarget:
                _state = PlayerState.SetTarget;
                break;
            case PlayerState.Chase:
                _animator.CrossFade("RUN", 0.1f);
                _state = PlayerState.Chase;
                break;
            case PlayerState.Attack:
                _state = PlayerState.Attack;
                break;
            case PlayerState.Dead:
                _state = PlayerState.Dead;
                break;
        }
    }

    AnimationClip GetAnimationClip(string animName)
	{
        AnimationClip clip = new AnimationClip();

        for(int i = 0; i < _animationClips.Length; i++)
		{
            if(_animationClips[i].name == animName)
			{
                clip = _animationClips[i];
                break;
			}
		}

        return clip;
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
            _cornerNum = 1;
            ChangeState(PlayerState.Idle);
            return;
        }
        Move();
    }
    #endregion

    #region UpdateSetTarget

    void UpdateSetTarget()
	{
        EmptyTarget();
        SetNewTarget();
        ChangeState(PlayerState.Chase);
        return;
    }

    void SetNewTarget()
    {
        _target = Spawners.Instance.GetClosestMonster();
        _targetAI = _target.GetComponent<MonsterAI>();

        GameObject targetMark = Pools.Instance.PoolDict[PoolType.TargetMark].Dequeue(_target.transform.position, _target.transform);
        targetMark.transform.position += Vector3.up * 0.1f;
        targetMark.transform.rotation = Quaternion.Euler(90, 0, 0);

        //targetMark.transform.SetParent(_target.transform);
        if (_target == null)
            Debug.Log("Couldn't Set New Target");
    }

    void SetNewTarget(GameObject target)
    {
        _target = target;
        _targetAI = _target.GetComponent<MonsterAI>();

        GameObject targetMark = Pools.Instance.PoolDict[PoolType.TargetMark].Dequeue(_target.transform.position, _target.transform);
        targetMark.transform.position += Vector3.up * 0.1f;
        targetMark.transform.rotation = Quaternion.Euler(90, 0, 0);

        //targetMark.transform.SetParent(_target.transform);
        if (_target == null)
            Debug.Log("Couldn't Set New Target");
    }

    public void EmptyTarget()
	{
        _target = null;
        _targetAI = null;
	}
    #endregion


    #region UpdateChase
    void UpdateChase()
    {
        if (IsTargetInRange(_target))
        {
            _cornerNum = 1;
            ChangeState(PlayerState.Attack);
            return;
        }
        else
        {
            if(_skillHolder.IsAnimPlaying == false)
                MoveToTarget();
        }
    }

    bool IsTargetInRange(GameObject target)
    {
        if(target == null)
		{
            Debug.Log($"Target is NULL!! {gameObject.name}");
            return false;
		}

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
            new Vector2(target.transform.position.x, target.transform.position.z)) <= _attackRange)
            return true;
        return false;
    }

    void MoveToTarget()
	{
        SetPath(_target.transform.position);
        Move();
	}
	#endregion

	#region UpdateAttack
    void UpdateAttack()
	{
        if (_target == null)
        {
            if (_skillHolder.IsAnimPlaying == false)
            {
                ChangeState(PlayerState.SetTarget);
                return;
            }
        }
        else
        {
            if (_targetAI.State == MonsterState.Dead)
            {
                if (_skillHolder.IsAnimPlaying == false)
                {
                    if (_isAutoMode)
                    {
                        ChangeState(PlayerState.SetTarget);
                    }
                    else
                    {
                        ChangeState(PlayerState.Idle);
                    }
                    return;
                }
            }
            else
            {
                if (IsTargetInRange(_target) == false)
                {
                    if (_skillHolder.IsAnimPlaying == false)
                    {
                        ChangeState(PlayerState.Chase);
                        return;
                    }
                }
                else
                {
                    LookAtTarget(_target.transform.position);
                    _skillHolder.AutoMode();
                }
            }
        }
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
        if (Input.GetMouseButton(0))// && _skillHolder.IsAnimPlaying == false)
        {
            if (Utils.IsUIHit())
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (_target != null)
                {
                    if (_state == PlayerState.Attack || _state == PlayerState.Chase)
                        _target.GetComponent<MonsterAI>().RemoveTargetMark();
                }

                if (hit.transform.tag == "Ground")
                {
                    SetPath(hit.point);
                    ChangeState(PlayerState.Move);
                    return;
                }
                else if (hit.transform.tag == "Enemy")
                {
                    Debug.Log("Enemy Clicked");
                    SetNewTarget(hit.transform.gameObject);
                    ChangeState(PlayerState.Chase);
                    return;
                }
            }
        }
	}

    void SetPath(Vector3 destination)
	{
		_destination = new Vector3(destination.x, transform.position.y, destination.z);
        if (NavMesh.CalculatePath(transform.position, _destination, NavMesh.AllAreas, _path))
        {
            if (_path.corners.Length == 2)
            {
                _corners = _path.corners;
                _cornerNum = 1;
            }
            else
			{
                if(_cornerNum < 2)
				{
                    _corners = _path.corners;
                    _cornerNum = 1;
				}
                else
				{
                    _corners = _path.corners;
				}
			}
            //Debug.Log($"cornernum : {_corners.Length}");
            return;
        }
    }

	void Move()
	{
        if (_corners != null && _corners.Length > 0)
        {
            LookAtTarget(_targetPos);
            _targetPos = _corners[_cornerNum];

            if(!HasArrivedTo(_destination))
			{
                if(HasArrivedTo(_corners[_cornerNum]))
				{
                    if (_cornerNum <= _corners.Length - 1)
                        _cornerNum++;
                }
                transform.position += (_corners[_cornerNum] - transform.position).normalized * Time.deltaTime * _moveSpeed;
			}

            //if (!HasArrivedTo(_destination) && HasArrivedTo(_corners[_cornerNum]))
            //{
            //    if (_cornerNum <= _corners.Length - 1)
            //        _cornerNum++;
            //}
            //else if (!HasArrivedTo(_destination))
            //    transform.position += (_corners[_cornerNum] - transform.position).normalized * Time.deltaTime * _moveSpeed;
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

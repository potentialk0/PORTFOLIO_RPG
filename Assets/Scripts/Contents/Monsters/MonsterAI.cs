using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterState
{
    Idle,
    Roam,
    Chase,
    BattleIdle,
    Attack,
    Dead,
    Num,
}

public class MonsterAI : MonoBehaviour
{
    protected Animator _animator;

    [SerializeField] protected MonsterState _state;

    protected Vector3 _spawnPoint;

    // 이동
    protected NavMeshPath _navMeshPath;
    protected Vector3 _destination;
    protected Vector3 _targetPos;

    protected Vector3[] _corners;
    protected int _cornerNum = 0;
    protected float _arriveOffset = 0.2f;

    protected float _rotSpeed = 10f;
    [SerializeField] protected float _moveSpeed;

    // idle
    protected float _idleTimer = 0;
    [SerializeField] protected float _idleTime;

    // roam
    [SerializeField] protected float _roamRange;
    protected Vector3 _roamPosition;

    // attack
    protected GameObject _player;
    protected StatContainer _playerStat;
    protected StatContainer _monsterStat;

    [SerializeField] protected float _attackCooltime;
    protected float _attackTimer = 0;
    [SerializeField] protected float _attackRange;
    protected float _attackAnimTimer = 0;
    protected float _attackAnimCooltime;

    // dead
    protected float _dieAnimTimer = 0;
    protected float _dieAnimLength;
    protected Material _material;
    protected float _fadeSpeed = 0.3f;

    [SerializeField] protected float _crossFadeIdle;
    [SerializeField] protected float _crossFadeRoam;
    [SerializeField] protected float _crossFadeChase;
    [SerializeField] protected float _crossFadeBattleIdle;
    [SerializeField] protected float _crossFadeAttack;
    [SerializeField] protected float _crossFadeDie;

    // Start is called before the first frame update
    protected void Start()
    {
        _animator = GetComponent<Animator>();
        _state = MonsterState.Idle;
        _spawnPoint = transform.position;
        _navMeshPath = new NavMeshPath();

        _player = GameObject.FindGameObjectWithTag("Player");
        _playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<StatContainer>();
        _monsterStat = GetComponent<StatContainer>();

        _attackAnimCooltime = _animator.runtimeAnimatorController.animationClips[0].length;
        _attackCooltime = _attackCooltime - _attackAnimCooltime;

        _dieAnimLength = _animator.runtimeAnimatorController.animationClips[5].length;
        _material = GetComponentInChildren<SkinnedMeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Test();
        ProcessState();
    }

    void Test()
	{
        if (Input.GetKeyDown(KeyCode.R))
            ChangeState(MonsterState.Chase);
        if (Input.GetKeyDown(KeyCode.T))
            ChangeState(MonsterState.Roam);
        if (Input.GetKeyDown(KeyCode.Q))
            ChangeState(MonsterState.Dead);
    }

    void ProcessState()
	{
        CheckIsDead();
        switch (_state)
        {
            case MonsterState.Idle:
                UpdateIdle();
                break;
            case MonsterState.Roam:
                UpdateRoam();
                break;
            case MonsterState.Chase:
                UpdateChase();
                break;
            case MonsterState.BattleIdle:
                UpdateBattleIdle();
                break;
            case MonsterState.Attack:
                UpdateAttack();
                break;
            case MonsterState.Dead:
                UpdateDie();
                break;
        }
    }

    public void ChangeState(MonsterState monsterState)
	{
        if (_state == monsterState) return;

        switch(monsterState)
		{
            case MonsterState.Idle:
                _animator.CrossFade("IDLE", _crossFadeIdle);
                _state = MonsterState.Idle;
                break;
            case MonsterState.Roam:
                _animator.CrossFade("WALK", _crossFadeRoam);
                SetPath(RoamPosition());
                _state = MonsterState.Roam;
                break;
            case MonsterState.Chase:
                ResetAnimtime();
                _animator.CrossFade("RUN", _crossFadeChase);
                _state = MonsterState.Chase;
                break;
            case MonsterState.BattleIdle:
                _animator.CrossFade("BATTLEIDLE", _crossFadeBattleIdle);
                _state = MonsterState.BattleIdle;
                break;
            case MonsterState.Attack:
                _animator.CrossFade("ATTACK", _crossFadeAttack);
                _state = MonsterState.Attack;
                break;
            case MonsterState.Dead:
                _animator.CrossFade("DIE", _crossFadeDie);
                _state = MonsterState.Dead;
                break;
        }
	}

	#region UpdateIdle
	void UpdateIdle()
	{
        //if (공격받으면)
        //{
        //    ChangeState(MonsterState.Chase);
        //    return;
        //}

        _idleTimer += Time.deltaTime;
        if (_idleTimer > _idleTime)
        {
            _idleTimer = 0;
            ChangeState(MonsterState.Roam);
            return;
        }
	}
    #endregion

    #region UpdateRoam
    void UpdateRoam()
	{
        //if (공격받으면)
        //{
        //    ChangeState(MonsterState.Chase);
        //    return;
        //}

        // 중립 몬스터 : 공격당하면 state == combat
        // 호전적 몬스터 : 시야 범위 내에 player가 들어오면 state == combat
        if (HasArrivedTo(_roamPosition))
        {
            ChangeState(MonsterState.Idle);
            return;
        }
        Move();
	}

    Vector3 RoamPosition()
	{
        float roamOffsetX = Random.Range(-_roamRange, _roamRange);
        float roamOffsetZ = Random.Range(-_roamRange, _roamRange);
        _roamPosition = new Vector3(_spawnPoint.x + roamOffsetX, transform.position.y, _spawnPoint.z + roamOffsetZ);
        return _roamPosition;
	}
    #endregion

    #region UpdateChase
    void UpdateChase()
    {
        if (!IsRoamArea())
        {
            ChangeState(MonsterState.Roam);
            return;
        }

        if (IsInAttackRange())
		{
            ChangeState(MonsterState.Attack);
            return;
		}
        else if (IsInAttackRange())
		{
            ChangeState(MonsterState.BattleIdle);
		}

        SetPath(_player.transform.position);
        Move();
    }
    #endregion

    #region UpdateBattleIdle
    void UpdateBattleIdle()
    {
        if (!IsInAttackRange())
        {
            ChangeState(MonsterState.Chase);
            return;
        }
        else
		{
            _attackTimer += Time.deltaTime;

            if (_attackTimer >= _attackCooltime)
            {
                _attackTimer = 0;
                ChangeState(MonsterState.Attack);
            }
            return;
		}
    }
    #endregion

    #region UpdateAttack
    
    void UpdateAttack()
	{
        if (!IsRoamArea())
        {
            ChangeState(MonsterState.Roam);
            return;
        }

        LookAtTarget(_player.transform.position);

        if(!IsInAttackRange())
		{
            ChangeState(MonsterState.Chase);
            return;
		}
        else
        {
            _attackAnimTimer += Time.deltaTime;
            if (_attackAnimTimer >= _attackAnimCooltime)
            {
                _attackAnimTimer = 0;
                ChangeState(MonsterState.BattleIdle);
                return;
            }
        }
	}

    void Damage()
	{
        _playerStat.GetDamage(_monsterStat);
	}
    #endregion

    #region UpdateDie
    void UpdateDie()
	{
        _dieAnimTimer += Time.deltaTime;
        if(_dieAnimTimer >= _dieAnimLength)
		{
            Color temp = _material.color;
            float fade = temp.a - (_fadeSpeed * Time.deltaTime);

            temp = new Color(temp.r, temp.b, temp.g, fade);
            _material.color = temp;

            if (fade <= 0)
                Destroy(gameObject);
		}
	}
    #endregion

    void SetPath(Vector3 destination)
    {
        _cornerNum = 1;
        _destination = new Vector3(destination.x, transform.position.y, destination.z);
        if (NavMesh.CalculatePath(transform.position, _destination, NavMesh.AllAreas, _navMeshPath))
        {
            _corners = _navMeshPath.corners;
            return;
        }
    }

    void Move()
    {
        if (_corners != null && _corners.Length > 0)
        {
            _targetPos = _corners[_cornerNum];
            LookAtTarget(_targetPos);
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
    void CheckIsDead()
    {
        if (_monsterStat.CurrHP <= 0)
            ChangeState(MonsterState.Dead);
    }

    void ResetAnimtime()
    {
        float animOffset = _attackAnimCooltime - _attackAnimTimer;
        _attackAnimTimer = 0;
        _attackTimer += animOffset;
    }

    bool IsInAttackRange()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < _attackRange)
            return true;
        else
            return false;
    }

    bool IsRoamArea()
    {
        float currentDistance = Vector3.Distance(_spawnPoint, transform.position);
        if (currentDistance <= _roamRange)
            return true;
        else
            return false;
    }
}

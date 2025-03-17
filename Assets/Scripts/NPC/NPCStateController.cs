using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IState
{
    public void StateEnter();
    public void StateUpdate();
    public void StateExit();
}

public enum NPCState
{
    Idle,
    Wander,
    Chase,
    Attack,
    Run
}

public class NPCStateController : MonoBehaviour
{
    enum CombatAttitude
    {
        Chase,
        Flee
    }

    [Header("State")]
    [SerializeField] private CombatAttitude _combatAttitud;
    [SerializeField] private string[] _stateKeys;

    [Space, Header("대기 상태 유지 시간")]
    [SerializeField] private float _minStateChangeTime;
    [SerializeField] private float _maxStateChangeTime;

    [Space, Header("이동 속도")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    [Space, Header("Wander")]
    [SerializeField] private float _minWanderDistance;
    [SerializeField] private float _maxWanderDistance;

    [Space, Header("Attack")]
    [SerializeField] private float _attackRate;

    private Animator _anim;
    private NavMeshAgent _agent;
    private NPCCombat _combat;
    public NPCCombat Combat { get { return _combat; } }
    private IState _curState;
    [SerializeField] private NPCState _npcState = NPCState.Idle;
    public NPCState CurNPCState { get { return _npcState; } }

    private Dictionary<string, IState> _dicState;
    private List<IState> _listState;

    private readonly int Idle = Animator.StringToHash("Idle");
    private readonly int Walk = Animator.StringToHash("Walk");
    private readonly int Attack = Animator.StringToHash("Attack");

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _dicState = new Dictionary<string, IState>();
        Init();
    }

    private void Init()
    {
        //_listState = new List<IState>
        //{
        //    new NPCIdleState(this, _minStateChangeTime, _maxStateChangeTime),
        //    new NPCWanderState(this, _agent, _minWanderDistance, _maxWanderDistance, _walkSpeed)
        //};

        _dicState.Add(_stateKeys[0], new NPCIdleState(this, _minStateChangeTime, _maxStateChangeTime));
        _dicState.Add(_stateKeys[1], new NPCWanderState(this, _agent, _minWanderDistance, _maxWanderDistance, _walkSpeed));

        if(_combatAttitud == CombatAttitude.Chase)
        {
            //_listState.Add(new NPCChaseState(this, _agent, _runSpeed));
            //_listState.Add(new NPCAttackState(this, _attackRate));
            _dicState.Add(_stateKeys[2], new NPCChaseState(this, _agent, _runSpeed));
            _dicState.Add(_stateKeys[3], new NPCAttackState(this, _attackRate));
        }

        StateChange(_npcState);
    }

    private void Update()
    {
        _curState.StateUpdate();
    }

    public void StateChange(NPCState nextState)
    {
        if (_curState != null)
        {
            _curState.StateExit();
        }

        _npcState = nextState;

        switch(_npcState)
        {
            case NPCState.Idle:
                _anim.SetBool(Idle, true);
                _anim.SetBool(Walk, false);
                //_curState = _listState[0];
                _curState = _dicState[_stateKeys[0]];
                break;

            case NPCState.Wander:
                _anim.SetBool(Idle, true);
                _anim.SetBool(Walk, true);
                //_curState = _listState[1];
                _curState = _dicState[_stateKeys[1]];
                break;

            case NPCState.Chase:
            case NPCState.Run:
                _anim.SetBool(Idle, false);
                //_curState = _dicState[_stateKeys[2]];
                _curState = _dicState[_stateKeys[2]];
                break;

            case NPCState.Attack:
                //_curState = _dicState[_stateKeys[3]];
                _anim.SetTrigger(Attack);
                _curState = _dicState[_stateKeys[3]];
                break;
        }
        
        _curState.StateEnter();
    }
}
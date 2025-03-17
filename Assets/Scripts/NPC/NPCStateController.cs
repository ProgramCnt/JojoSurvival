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

public enum CombatAttitude
{
    Chase,
    Flee
}

public class NPCStateController : MonoBehaviour
{

    [Header("State")]
    [SerializeField] private CombatAttitude _combatAttitud;
    public CombatAttitude CA { get { return _combatAttitud; } }
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

    private readonly int Idle = Animator.StringToHash("Idle");
    private readonly int Walk = Animator.StringToHash("Walk");
    private readonly int Attack = Animator.StringToHash("Attack");

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _dicState = new Dictionary<string, IState>();
    }

    private void Start()
    {
        Init();
        
    }

    private void Init()
    {
        _dicState.Add(_stateKeys[0], new NPCIdleState(this, _minStateChangeTime, _maxStateChangeTime));
        _dicState.Add(_stateKeys[1], new NPCWanderState(this, _agent, _minWanderDistance, _maxWanderDistance, _walkSpeed));

        if(_combatAttitud == CombatAttitude.Chase)
        {
            _dicState.Add(_stateKeys[2], new NPCChaseState(this, _agent, _runSpeed));
            _dicState.Add(_stateKeys[3], new NPCAttackState(this, _attackRate));
        }
        else
        {
            _dicState.Add(_stateKeys[2], new NPCFleeState(this, _agent, _runSpeed));
        }

        StateChange(_npcState);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _minWanderDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _maxWanderDistance);
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
                _agent.isStopped = true;
                _agent.speed = 1;
                _curState = _dicState[_stateKeys[0]];
                break;

            case NPCState.Wander:
                _anim.SetBool(Idle, true);
                _anim.SetBool(Walk, true);
                _curState = _dicState[_stateKeys[1]];
                break;

            case NPCState.Chase:
            case NPCState.Run:
                _anim.SetBool(Idle, false);
                _curState = _dicState[_stateKeys[2]];
                break;

            case NPCState.Attack:
                _anim.SetTrigger(Attack);
                _agent.isStopped = true;
                _agent.speed = 1f;
                _curState = _dicState[_stateKeys[3]];
                break;
        }
        
        _curState.StateEnter();
    }
}
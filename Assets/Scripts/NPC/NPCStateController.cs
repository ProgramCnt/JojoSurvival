using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 외부 스크립트에서 현재 상태를 파악하거나 변경하기 위해 사용
public enum NPCState
{
    Idle,
    Wander,
    Chase,
    Attack,
    Run
}

// 플레이어를 발견했을 때 보일 반응
public enum CombatAttitude
{
    Chase,
    Flee
}

public class NPCStateController : MonoBehaviour
{

    [Header("State")]
    [SerializeField] private CombatAttitude _combatAttitud;     // 이 npc가 플레이어를 발견했을 때 보일 행동
    public CombatAttitude CA { get { return _combatAttitud; } }
    [SerializeField] private string[] _stateKeys;   // 상태 dictionary의 키를 담는 배열

    [Space, Header("대기 상태 유지 시간")]
    [SerializeField] private float _minStateChangeTime;
    [SerializeField] private float _maxStateChangeTime;

    [Space, Header("이동 속도")]
    [SerializeField] private float _walkSpeed;      // 걷는 속도
    [SerializeField] private float _runSpeed;       // 달리는 속도

    [Space, Header("Wander")]
    [SerializeField] private float _minWanderDistance;      // 최소 배회 시간
    [SerializeField] private float _maxWanderDistance;      // 최대 배회 시간

    [Space, Header("Attack")]
    [SerializeField] private float _attackRate;     // 공격이 이어지는 시간 간격
    public float AttackRate { get { return _attackRate; } }

    private Animator _anim;
    private NavMeshAgent _agent;
    private NPCCombat _combat;
    public NPCCombat Combat { get { return _combat; } }
    
    private IState _curState;   // 현재 상태의 상태 클래스를 담을 변수
    [SerializeField] private NPCState _npcState = NPCState.Idle;    // 외부 스크립트에서 접근할 현재 상태 변수
    public NPCState CurNPCState { get { return _npcState; } }

    private Dictionary<string, IState> _dicState;   // 상태 클래스들을 담을 dictionary

    private readonly int Idle = Animator.StringToHash("Idle");
    private readonly int Walk = Animator.StringToHash("Walk");
    private readonly int Attack = Animator.StringToHash("Attack");

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _combat = GetComponent<NPCCombat>();
        _dicState = new Dictionary<string, IState>();
    }

    private void Start()
    {
        Init();    
    }

    /// <summary>
    /// _combatAttitude에 따른 상태 클래스 생성
    /// & _dicState에 추가
    /// </summary>
    private void Init()
    {
        // 어느 CombatAttitude에도 있는 대기 상태와 배회 상태의 상태 클래스 생성 & 딕셔너리에 추가
        _dicState.Add(_stateKeys[0], new NPCIdleState(this, _minStateChangeTime, _maxStateChangeTime));
        _dicState.Add(_stateKeys[1], new NPCWanderState(this, _agent, _minWanderDistance, _maxWanderDistance, _walkSpeed));

        // 플레이를 발견했을 때 추적하는 npc라면
        if(_combatAttitud == CombatAttitude.Chase)
        {
            // 추적과 공격 상태 클래스 생성 및 추가
            _dicState.Add(_stateKeys[2], new NPCChaseState(this, _agent, _runSpeed));
            _dicState.Add(_stateKeys[3], new NPCAttackState(this, _attackRate));
        }
        else
        {// 도망치는 npc라면
            // 도주 상태 클래스 생성 및 추가
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
        if (_curState != null)
        {
            _curState.StateUpdate();
        }

        if(_npcState == NPCState.Idle || _npcState == NPCState.Attack)
        {
            _agent.isStopped = true;
            _agent.velocity = Vector3.zero;
            _agent.updateRotation = false;
        }
        else
        {
            _agent.updateRotation = true;
        }
    }

    /// <summary>
    /// 상태 변경 함수
    /// </summary>
    /// <param name="nextState">어떤 상태로 변경할지</param>
    public void StateChange(NPCState nextState)
    {
        // 현재 상태가 null이 아니라면
        // 현재 상태의 상태 종료 함수 호출
        if (_curState != null)
        {
            _curState.StateExit();
        }

        _npcState = nextState;

        // 전달 받은 매개 변수에 따라서 상태&애니메이션 변경
        switch(_npcState)
        {
            case NPCState.Idle:
                _anim.SetBool(Idle, true);
                _anim.SetBool(Walk, false);
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
                _curState = _dicState[_stateKeys[3]];
                break;
        }
        
        // 상태 진입 함수 호출
        _curState.StateEnter();
    }
}
using UnityEngine;

public class Detect : MonoBehaviour
{
    [SerializeField, Tooltip("감지 범위")] private float _detectRange;
    [SerializeField, Tooltip("시야각")] private float _fieldOfView;
    [SerializeField, Tooltip("공격 범위")] private float _attackRange;

    private Transform _playerTrs;
    private NPCStateController _controller;

    private float _playerDistance;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * _detectRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }

    void Start()
    {
        _controller = GetComponent<NPCStateController>();

        if (CharacterManager.Instance.Player != null)
        {
            _playerTrs = CharacterManager.Instance.Player.transform;
        }
    }

    void Update()
    {
        DetectPlayer();
    }

    /// <summary>
    /// 플레이어 감지 함수
    /// </summary>
    void DetectPlayer()
    {
        if (_playerTrs == null) return;

        // 플레이어와의 거리
        _playerDistance = Vector3.Distance(transform.position, _playerTrs.position);
        
        // 플레이어가 감지 범위 안에 있고 시야각 안에 있다면
        if(_playerDistance < _detectRange && IsPlayerInFieldOfView())
        {
            // 플레이어 발견 시 추적하는 npc라면
            if(_controller.CA == CombatAttitude.Chase)
            { 
                // 플레이어가 공격 범위 안에 있다면
                if(_playerDistance < _attackRange)
                {
                    // 공격 상태로 전환
                    _controller.StateChange(NPCState.Attack);
                }
                else
                {// 공격 범위 밖이라면
                    // npc가 공격 상태라면
                    if (_controller.CurNPCState == NPCState.Attack)
                    {
                        // 2초 대기 후 추적 상태로
                        Invoke("ChangeChaseState", 2f);
                    }
                    else
                    {
                        _controller.StateChange(NPCState.Chase);
                    }
                }
            }
            else
            {
                _controller.StateChange(NPCState.Run);
            }
        }
        else
        {
            if (_controller.CurNPCState != NPCState.Run)
            {
                _controller.StateChange(NPCState.Idle);
            }
        }
    }

    void ChangeChaseState()
    {
        _controller.StateChange(NPCState.Chase);
    }

    private bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = _playerTrs.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < _fieldOfView * 0.5f;
    }
}

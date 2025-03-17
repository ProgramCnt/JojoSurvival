using UnityEngine;

public class Detect : MonoBehaviour
{
    [SerializeField] private float _detectRange;
    [SerializeField] private float _fieldOfView;
    [SerializeField] private float _attackRange;

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

    void DetectPlayer()
    {
        if (_playerTrs == null) return;

        _playerDistance = Vector3.Distance(transform.position, _playerTrs.position);
        
        if(_playerDistance < _detectRange && IsPlayerInFieldOfView())
        {
            if(_controller.CA == CombatAttitude.Chase)
            { 
                if(_playerDistance < _attackRange)
                {
                    _controller.StateChange(NPCState.Attack);
                }
                else
                {
                    if (_controller.CurNPCState == NPCState.Attack)
                    {
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

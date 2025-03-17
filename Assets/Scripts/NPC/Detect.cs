using UnityEngine;

public class Detect : MonoBehaviour
{
    [SerializeField] private float _detectRange;
    [SerializeField] private float _fieldOfView;
    [SerializeField] private float _attackRange;

    private Transform _playerTrs;
    private NPCStateController _controller;

    private float _playerDistance;

    void Start()
    {
        _playerTrs = CharacterManager.Instance.Player.transform;
        _controller = GetComponent<NPCStateController>();
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
            if(_playerDistance < _attackRange && _controller.CurNPCState != NPCState.Attack)
            {
                _controller.StateChange(NPCState.Attack);
            }
            else if(_controller.CurNPCState != NPCState.Chase)
            {
                _controller.StateChange(NPCState.Chase);
            }
        }
        else
        {
            _controller.StateChange(NPCState.Idle);
        }
    }

    private bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = _playerTrs.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < _fieldOfView * 0.5f;
    }
}

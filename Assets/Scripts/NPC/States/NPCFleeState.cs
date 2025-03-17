using UnityEngine;
using UnityEngine.AI;

public class NPCFleeState : MonoBehaviour, IState
{
    private NPCStateController _controller;
    private NavMeshAgent _agent;
    private Transform _playerTrs;

    private float _runSpeed;

    public NPCFleeState(NPCStateController _con, NavMeshAgent _ag, float _speed)
    {
        this._controller = _con;
        this._agent = _ag;
        this._runSpeed = _speed;

        if(CharacterManager.Instance.Player != null)
        {
            _playerTrs = CharacterManager.Instance.Player.transform;
        }
    }

    public void StateEnter()
    {
        _agent.isStopped = false;
        _agent.speed = _runSpeed;
    }

    public void StateExit()
    {
        _agent.isStopped = true;
    }

    public void StateUpdate()
    {
        if (_playerTrs != null)
        {
            if (Vector3.Distance(_controller.transform.position, _playerTrs.position) > 10f)
            {
                _controller.StateChange(NPCState.Idle);
            }
            else
            {
                _agent.SetDestination((_controller.transform.position - _playerTrs.position) * 10f);
            }
        }
    }
}

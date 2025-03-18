using UnityEngine;
using UnityEngine.AI;

public class NPCChaseState : IState
{
    private NPCStateController _controller;
    private NavMeshAgent _agent;
    private Transform _playerTrs;
    
    private float _runSpeed;

    public NPCChaseState(NPCStateController _con, NavMeshAgent _ag, float _speed)
    {
        this._controller = _con;
        this._agent = _ag;
        this._runSpeed = _speed;
        if (CharacterManager.Instance.Player != null)
        {
            _playerTrs = CharacterManager.Instance.Player.transform;
        }
    }

    public void StateEnter()
    {
        _agent.isStopped = false;
        _agent.speed = _runSpeed;
        ChasePlayer();
    }

    void ChasePlayer()
    {
        NavMeshPath path = new NavMeshPath();

        if(_playerTrs != null && _agent.CalculatePath(_playerTrs.position, path))
        {
            _agent.SetDestination(_playerTrs.position);
        }
        else
        {
            _controller.StateChange(NPCState.Idle);
        }
    }

    public void StateExit()
    {
        _agent.isStopped = true;
    }

    public void StateUpdate()
    {
        ChasePlayer();
    }
}

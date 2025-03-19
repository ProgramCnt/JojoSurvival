using UnityEngine;
using UnityEngine.AI;

public class NPCWanderState : MonoBehaviour, IState
{
    private NavMeshAgent _agent;
    private NPCStateController _controller;

    private float _walkSpeed = 3f;
    private float _minWanderDistance = 5f;
    private float _maxWanderDistance = 10f;

    public NPCWanderState(NPCStateController _controller, NavMeshAgent _ag, float _minDistance, float _maxDistance, float _speed)
    {
        this._controller = _controller;
        this._agent = _ag;
        this._minWanderDistance = _minDistance;
        this._minWanderDistance = _maxDistance;
        this._walkSpeed = _speed;
    }

    public void StateEnter()
    {
        _agent.isStopped = false;
        _agent.speed = _walkSpeed;
        WanderToNewLocation();
    }

    public void StateExit()
    {
        _agent.isStopped = true;
    }

    public void StateUpdate()
    {
        if (_agent.remainingDistance < 0.1f)
        {
            _controller.StateChange(NPCState.Idle);
        }
    }

    public void WanderToNewLocation()
    {
        _agent.speed = _walkSpeed;
        _agent.SetDestination(GetWanderLocation());
    }

    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;
        int i = 0;

        do
        {
            NavMesh.SamplePosition(_controller.transform.position + (Random.onUnitSphere
                * Random.Range(_minWanderDistance, _maxWanderDistance)), out hit, _maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30) break;
        } while (Vector3.Distance(_controller.transform.position, hit.position) < _minWanderDistance);

        return hit.position;
    }
}
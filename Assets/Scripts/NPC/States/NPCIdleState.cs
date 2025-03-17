using UnityEngine;

public class NPCIdleState : IState
{
    private NPCStateController _controller;

    [SerializeField] private float _minIdleTime = 1f;
    private float _maxIdleTime = 3f;
    private float _idleTime = 0f;

    public NPCIdleState(NPCStateController _con, float _min, float _max)
    {
        this._controller = _con;
        this._minIdleTime = _min;
        this._maxIdleTime = _max;
    }

    public void StateEnter()
    {
        _idleTime = Random.Range(_minIdleTime, _maxIdleTime);
    }

    public void StateExit()
    {
        
    }

    public void StateUpdate()
    {
        if(_idleTime <= 0)
        {
            _controller.StateChange(NPCState.Wander);
        }
        else
        {
            _idleTime -= Time.deltaTime;
        }
    }
}
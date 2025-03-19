using UnityEngine;

public class NPCIdleState : IState
{
    private NPCStateController _controller;

    private float _minIdleTime = 1f;
    private float _maxIdleTime = 3f;
    private float _idleTime = 0f;

    /// <summary>
    /// 생성자
    /// 변수 초기화
    /// </summary>
    /// <param name="_con">NPCStateController 스크립트</param>
    /// <param name="_min">대기 상태 최소 유지 시간</param>
    /// <param name="_max">대기 상태 최대 유지 시간</param>
    public NPCIdleState(NPCStateController _con, float _min, float _max)
    {
        this._controller = _con;
        this._minIdleTime = _min;
        this._maxIdleTime = _max;
    }

    public void StateEnter()
    {
        // 대기 상태 유지 시간 설정
        _idleTime = Random.Range(_minIdleTime, _maxIdleTime);
    }

    public void StateExit()
    {
        
    }

    public void StateUpdate()
    {
        // 대기 상태 시간이 끝나면 배회 상태로 변경
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
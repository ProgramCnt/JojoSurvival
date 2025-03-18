using UnityEngine;

public class NPCAttackState : IState
{
    private NPCStateController _controller;
    private NPCCombat _combat;

    private float _attackRate;
    private float _lastAttackTime = 0;

    public NPCAttackState(NPCStateController _con, float _rate)
    {
        this._controller = _con;
        this._combat = _controller.Combat;
        this._attackRate = _rate;
    }    

    public void StateEnter()
    {
        
    }

    public void StateExit()
    {
        
    }

    public void StateUpdate()
    {
        if(Time.time - _lastAttackTime > _attackRate)
        {
            _lastAttackTime = Time.time;
            _combat.Attack();
        }
    }
}

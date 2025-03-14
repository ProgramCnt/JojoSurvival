using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IState
{
    public void StateEnter();
    public void StateUpdate();
    public void StateExit();
}

public class NPCStateController : MonoBehaviour
{
    [SerializeField] protected float minStateChangeTime;
    [SerializeField] protected float maxStateChangeTime;
    
    protected IState curState;
    protected Animator animator;

    protected float _stateChangeTime;
    protected float lastStateChangeTime = 0f;

    public bool isDetect = false;

    protected readonly int Idle = Animator.StringToHash("Idle");
    protected readonly int Walk = Animator.StringToHash("Walk");

    protected void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Init();
    }

    protected void Init()
    {
        
    }

    protected void Update()
    {
        if(!isDetect)
        {
            if(Time.time - lastStateChangeTime > _stateChangeTime)
            {
                lastStateChangeTime = Time.time;
                _stateChangeTime = Random.Range(minStateChangeTime, maxStateChangeTime);
            }
        }
    }

    public void StateChange(IState _nextState)
    {
        curState.StateExit();
    }
}

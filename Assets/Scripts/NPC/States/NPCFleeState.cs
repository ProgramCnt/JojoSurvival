using UnityEngine;
using UnityEngine.AI;

public class NPCFleeState : MonoBehaviour, IState
{
    private NPCStateController _controller;
    private NavMeshAgent _agent;

    private float _runSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StateEnter()
    {
        throw new System.NotImplementedException();
    }

    public void StateExit()
    {
        throw new System.NotImplementedException();
    }

    public void StateUpdate()
    {
        throw new System.NotImplementedException();
    }
}

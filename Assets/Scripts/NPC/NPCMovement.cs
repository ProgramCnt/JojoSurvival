using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public bool isMove;

    private Transform _playerTrnasform;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(isMove)
        {
            Move();
        }
    }

    void Move()
    {

    }
}

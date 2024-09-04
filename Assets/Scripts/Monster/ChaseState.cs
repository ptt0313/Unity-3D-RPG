using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : MonoBehaviour, IState
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;
    private Enemy enemy;
    public void EnterState()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();


    }


    public void UpdateState()
    {
        
    }
    public void ExitState()
    {
        
    }

}

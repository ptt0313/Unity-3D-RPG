using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : MonoBehaviour, IState
{
    public GameObject[] walkPoints;
    int currentEnemyPosition = 0;
    float walkingPointRadius = 2;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Enemy enemy;

    public void EnterState()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();

        animator.SetTrigger("Walk");
        
    }


    public void UpdateState()
    {
        if (Vector3.Distance(walkPoints[currentEnemyPosition].transform.position,transform.position) < walkingPointRadius)
        {
            currentEnemyPosition = UnityEngine.Random.Range(0,walkPoints.Length);
            {
                if(currentEnemyPosition >= walkPoints.Length)
                {
                    currentEnemyPosition = 0;
                }
            }
        }
        navMeshAgent.SetDestination(walkPoints[currentEnemyPosition].transform.position);
    }
    public void ExitState()
    {
        
    }
    
}

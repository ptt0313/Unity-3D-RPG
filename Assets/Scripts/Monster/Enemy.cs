using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy States")]
    [SerializeField] IdleState idleState;
    [SerializeField] PatrolState patrolState;
    [SerializeField] ChaseState chaseState;
    [SerializeField] AttackState attackState;
    [SerializeField] DieState dieState;

    Transform transform;
    Transform playerTransform;
    private StateMachine stateMachine;
    private void Awake()
    {
        stateMachine = new StateMachine(this);
        stateMachine.Transition(idleState);
    }
    private void Start()
    {
        transform = this.gameObject.GetComponent<Transform>();
    }
    private void Update()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        if(Vector3.Distance(transform.position, playerTransform.position) > 20.0f)
        {
            UpdateState(_State.Idle);
        }
        else if (Vector3.Distance(transform.position, playerTransform.position) > 4.0f)
        {
            UpdateState(_State.Chase);
        }
        else
        {
            UpdateState(_State.Attack);
        }
        stateMachine.CurrentState.UpdateState();
    }
    private void UpdateState(_State state)
    {
        switch (state)
        {
            case _State.Idle: stateMachine.Transition(idleState);
                break;
            case _State.Chase: stateMachine.Transition(chaseState);
                break;
            case _State.Attack: stateMachine.Transition(attackState);
                break;
            case _State.Die: stateMachine.Transition(dieState);
                break;
        }
    }
}

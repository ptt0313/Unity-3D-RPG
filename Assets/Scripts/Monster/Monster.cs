using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    enum State
    {
        Idle,
        Move,
        Attack,
        Sturn,
        Die,
        Hit
    }
public class Monster : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Animator animator;
    [SerializeField] GameObject destination;

    private State state;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        state = State.Idle;
        destination = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle: Idle();
                break;
            case State.Move: Move();
                break;
            case State.Attack: Attack();
                break;
            case State.Hit: Hit();
                break;
            case State.Die: Die();
                break;
        }

    }

    private void Die()
    {
        animator.Play("Die");
    }

    private void Hit()
    {
        animator.Play("Hit");
    }

    private void Attack()
    {
        navMeshAgent.isStopped = true;
        animator.SetTrigger("Attack");
        transform.LookAt(new Vector3(destination.transform.position.x, transform.position.y, destination.transform.position.z));
        if (Vector3.Distance(transform.position, destination.transform.position) >= 2)
        {
            state = State.Move;
        }
    }

    private void Move()
    {
        navMeshAgent.isStopped = false;
        animator.SetTrigger("Move");
        navMeshAgent.SetDestination(destination.transform.position);
        transform.LookAt(new Vector3(destination.transform.position.x, transform.position.y, destination.transform.position.z));
        if (Vector3.Distance(transform.position, destination.transform.position) < 2)
        {
            state = State.Attack;
        }
        else if(Vector3.Distance(transform.position, destination.transform.position) >= 15)
        {
            state = State.Idle;
        }
    }

    private void Idle()
    {
        if (Vector3.Distance(transform.position, destination.transform.position) < 15)
        {
            state = State.Move;
        }
    }
    private void OnParticleTrigger()
    {
        Debug.Log("파티클 충돌");
    }
}

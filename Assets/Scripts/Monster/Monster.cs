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
        Die,
    }
public class Monster : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject player;
    [SerializeField] protected Collider playerWeapon;
    [SerializeField] protected BasePlayerState playerState;
    [SerializeField] protected Collider playerHitBox;

    private bool isInteracting;
    State state;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        state = State.Idle;
        player = GameObject.FindGameObjectWithTag("Player");
        playerWeapon = GameObject.Find("LongSwordMesh").GetComponent<Collider>();
        playerHitBox = GameObject.FindGameObjectWithTag("Hit Box").GetComponent<Collider>();
    }

    void Update()
    {
        isInteracting = animator.GetBool("isInteracting");
        switch (state)
        {
            case State.Idle: Idle();
                break;
            case State.Move: Move();
                break;
            case State.Attack: Attack();
                break;
        }

    }

    protected void Die()
    {
        state = State.Die;
        animator.Play("Die");
        StartCoroutine(Remove());
    }


    protected void Attack()
    {
        animator.SetTrigger("Attack");
        navMeshAgent.SetDestination(transform.position);
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        if (Vector3.Distance(transform.position, player.transform.position) >= 1.5f && isInteracting == false)
        {
            state = State.Move;
        }
    }

    protected void Move()
    {
        animator.SetTrigger("Move");
        navMeshAgent.SetDestination(player.transform.position);
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f)
        {
            state = State.Attack;
        }
        else if(Vector3.Distance(transform.position, player.transform.position) >= 15)
        {
            state = State.Idle;
        }
    }

    protected void Idle()
    {
        navMeshAgent.SetDestination(transform.position);
        animator.SetTrigger("Idle");
        if (Vector3.Distance(transform.position, player.transform.position) < 15)
        {
            state = State.Move;
        }
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(10);
        gameObject.SetActive(false);
    }
}

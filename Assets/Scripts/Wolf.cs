using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Monster
{
    enum State
    {
        Idle,
        Move,
        Attack
    }
    State curState;
    FSM fsm;

    private void Start()
    {
        curState = State.Idle;
        fsm = new FSM(new IdleState(this));
    }
    private void Update()
    {
        switch (curState)
        {
            case State.Idle:
                if(CanSeePlayer())
                {
                    if(CanAttackPlayer())
                    {
                        ChageState(State.Attack);
                    }
                    else
                    {
                        ChageState(State.Move);
                    }
                }
                break;
            case State.Move:
                if(CanSeePlayer())
                {
                    if(CanAttackPlayer())
                    {
                        ChageState(State.Attack);
                    }
                }
                else
                {
                    ChageState(State.Idle);
                }
                break;
            case State.Attack:
                if(CanSeePlayer())
                {
                    if(!CanAttackPlayer())
                    {
                        ChageState(State.Move);
                    }
                }
                else
                {
                    ChageState(State.Idle);
                }
                break;
        }
        fsm.UpdateState();
    }
    private void ChageState(State nextState)
    {
        curState = nextState;
        switch(curState)
        {
            case State.Idle:
                fsm.ChageState(new IdleState(this));
                break;
            case State.Move:
                fsm.ChageState(new MoveState(this));
                break;
            case State.Attack:
                fsm.ChageState(new AttackState(this));
                break;
        }
    }
    private bool CanSeePlayer()
    {
        return true;
    }
    private bool CanAttackPlayer()
    {
        return true;
    }
}

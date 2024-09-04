using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum _State
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Die
}
public class StateMachine
{
    public IState CurrentState { get; set; }
    private readonly Enemy _controller;
    public StateMachine(Enemy controller)
    {
        _controller = controller;
    }
    public void Transition(IState state)
    {
        if (CurrentState != null) CurrentState.ExitState();
        CurrentState = state;
        CurrentState.EnterState();
    }
}

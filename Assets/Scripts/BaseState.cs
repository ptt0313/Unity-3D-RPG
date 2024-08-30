using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : StateMachineBehaviour
{
    protected Monster monster;

    protected BaseState(Monster monster)
    {
        this.monster = monster;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}


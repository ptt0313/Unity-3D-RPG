using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : StateMachineBehaviour
{
   
    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    public FSM(BaseState initState)
    {
        curState = initState;
        ChageState(curState);
    }
    BaseState curState;

    public void ChageState(BaseState nextState)
    {
        if (nextState == curState) return;
        if (curState != null) curState.OnStateExit();

        curState = nextState;
        curState.OnStateEnter();
    }
    public void UpdateState()
    {
        if (curState != null) curState.OnStateUpdate();
    }
}

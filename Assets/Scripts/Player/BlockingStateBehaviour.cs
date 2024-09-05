using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingStateBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetTrigger("BlockingOff");
        }
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}

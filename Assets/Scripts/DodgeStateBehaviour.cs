using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeStateBehaviour : StateMachineBehaviour
{
    bool isMove;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Enter Dodge State");
        animator.SetBool("Action", true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isMove = animator.GetBool("Move");
        if (isMove)
        {
            animator.SetTrigger("Run");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exit Dodge State");
        animator.SetBool("Action", false);

    }
}

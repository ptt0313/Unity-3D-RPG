using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRunStateBehaviour : StateMachineBehaviour
{
    bool isMove;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Enter SwordRunning State");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isMove = animator.GetBool("Move");
        if (!isMove)
        {
            animator.SetTrigger("Idle");
        }
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exit SwordRunning State");
    }
}

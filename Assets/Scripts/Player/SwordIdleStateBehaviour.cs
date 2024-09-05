using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordIdleStateBehaviour : StateMachineBehaviour
{
    bool isMove;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isMove = animator.GetBool("Move");
        if (isMove)
        {
            animator.SetTrigger("Run");
        }
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
        if(Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("ChargeAttack");
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            animator.SetBool("Armed", false);
            animator.SetTrigger("Sheath");
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetTrigger("Blocking");
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}

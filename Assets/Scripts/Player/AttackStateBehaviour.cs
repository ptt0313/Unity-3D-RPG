using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackStateBehaviour : StateMachineBehaviour
{
    float comboTime = 1.0f;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    public  override  void  OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (comboTime >= 0)
        {
            comboTime -= Time.deltaTime;
        }
        if (comboTime > 0 && Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
        else if(comboTime == 0)
        {
            animator.SetTrigger("Idle");
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}

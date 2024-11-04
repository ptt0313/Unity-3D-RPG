using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorState : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnAttackJudgement()
    {
        animator.SetBool("isAttacking", true);
    }    
    void OffAttackJudgement()
    {
        animator.SetBool("isAttacking", false);
    }
    void OnRollJudgement()
    {
        animator.SetBool("isRolling", true);
    }
    void OffRollJudgement()
    {
        animator.SetBool("isRolling", false);
    }
}

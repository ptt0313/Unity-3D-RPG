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
    void OnIntrtacting()
    {
        animator.SetBool("isInteracting", true);
    }
    void OffInteracting()
    {
        animator.SetBool("isInteracting", false);
    }
    void OnAttackSount()
    {
        SoundManager.Instance.PlayEffect("Attack2");
        SoundManager.Instance.PlayEffect("Sword2");
    }
}

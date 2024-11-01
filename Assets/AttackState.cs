using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour
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

}

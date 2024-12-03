using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Monster
{
    [SerializeField] BaseMonsterStatus monsterStatus;
    [SerializeField] int hp;
    [SerializeField] int attack;
    [SerializeField] int defence;
    [SerializeField] int rewardExp;
    [SerializeField] int rewardGold;

    private BoxCollider boxCollider;
    void Awake()
    {
        hp = monsterStatus.Hp;
        attack = monsterStatus.AttackPoint;
        defence = monsterStatus.DefencePoint;
        rewardExp = monsterStatus.rewardExp;
        rewardGold = monsterStatus.rewardGold;
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (isDie == true)
        {
            return;
        }
        if (player.GetComponent<Animator>().GetBool("isRolling") == false && animator.GetBool("isAttacking") == true && other == playerHitBox && animator.GetBool("AttackCount") == false)
        {
            player.GetComponent<Animator>().Play("Hit");
            animator.SetBool("AttackCount", true);

            if ((attack - playerState.defencePoint) < 0)
            {
                return;
            }
            else
            {
                playerState.hp -= attack - playerState.defencePoint;
            }
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (player.GetComponent<Animator>().GetBool("isAttacking") == true && other == playerWeapon )
        {
            animator.Play("Hit");

            if ((playerState.attackPoint - defence) < 0)
            {
                return;
            }
            else
            {
                hp -= playerState.attackPoint - defence;
                if (hp <= 0)
                {
                    Die();
                    Reward();
                    SoundManager.Instance.PlayEffect("WolfDie");
                    boxCollider.enabled = false;
                }
            }
        }
    }
    private void Reward()
    {
        playerState.currentExp += rewardExp;
        playerState.gold += rewardGold;
    }
    void Attack()
    {
        base.Attack();
        SoundManager.Instance.PlayEffect("WolfAttack");
    }
}

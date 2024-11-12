using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Monster
{
    [SerializeField] BaseMonsterStatus monsterStatus;
    [SerializeField] int hp;
    [SerializeField] int attack;
    [SerializeField] int defence;
    [SerializeField] int rewardExp;
    [SerializeField] int rewardGold;

    private Collider capsuleCollider;
    void Awake()
    {
        hp = monsterStatus.Hp;
        attack = monsterStatus.AttackPoint;
        defence = monsterStatus.DefencePoint;
        rewardExp = monsterStatus.rewardExp;
        rewardGold = monsterStatus.rewardGold;
        capsuleCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (player.GetComponent<Animator>().GetBool("isAttacking") == true && other == playerWeapon)
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
                    SoundManager.Instance.PlayEffect("SpiderDie");
                    capsuleCollider.enabled = false;
                }
            }
        }
        if (player.GetComponent<Animator>().GetBool("isRolling") == false && animator.GetBool("isAttacking") == true && other == playerHitBox)
        {
            player.GetComponent<Animator>().Play("Hit");

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

    void Reward()
    {
        playerState.currentExp += rewardExp;
        playerState.gold += rewardGold;
    }
    new void Attack()
    {
        base.Attack();
        SoundManager.Instance.PlayEffect("SpiderAttack");
    }
}

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
    void Awake()
    {
        hp = monsterStatus.Hp;
        attack = monsterStatus.AttackPoint;
        defence = monsterStatus.DefencePoint;
        rewardExp = monsterStatus.rewardExp;
        rewardGold = monsterStatus.rewardGold;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (player.GetComponent<Animator>().GetBool("isAttacking") == true && other == playerWeapon)
        {
            if((playerState.attackPoint - defence) < 0)
            {
                return;
            }
            else
            {
                animator.Play("Hit");
                hp -= playerState.attackPoint - defence;
                if (hp <= 0)
                {
                    Die();
                    Reward();
                }
            }
        }
        if (player.GetComponent<Animator>().GetBool("isRolling") == false && animator.GetBool("isAttacking") == true && other == playerHitBox)
        {
            if((playerState.defencePoint - attack) < 0)
            {
                return;
            }
            else
            {
                player.GetComponent<Animator>().Play("Hit");
                playerState.hp -= attack - playerState.defencePoint;
            }
        }
    }

    void Reward()
    {
        playerState.currentExp += rewardExp;
        playerState.gold += rewardGold;
    }
}

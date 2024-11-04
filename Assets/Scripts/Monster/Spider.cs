using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Monster
{
    [SerializeField] BaseMonsterStatus monsterStatus;

    private Animator animator;

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
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (player.GetComponent<Animator>().GetBool("isAttacking") == true && other == playerWeapon)
        {
            animator.Play("Hit");
            hp -= playerState.attackPoint - defence;
        }
        if (player.GetComponent<Animator>().GetBool("isRolling") == false && animator.GetBool("isAttacking") == true && other.CompareTag("Player"))
        {
            player.GetComponent<Animator>().Play("Hit");
            playerState.hp -= attack - playerState.defencePoint;
        }
    }
    private void LateUpdate()
    {
        if (hp <= 0)
        {
            Die();
        }
    }
}

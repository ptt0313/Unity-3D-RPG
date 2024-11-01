using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterState", menuName = "Monster / State")]
public class BaseMonsterStatus : ScriptableObject
{
    public int Hp;
    public int AttackPoint;
    public int DefencePoint;
    public int rewardExp;
    public int rewardGold;
}

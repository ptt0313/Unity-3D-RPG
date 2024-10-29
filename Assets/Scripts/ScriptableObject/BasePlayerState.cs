using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "Player / State")]
public class BasePlayerState : ScriptableObject
{
    public List<ItemData> items = new List<ItemData>();
    
    public int level;

    public float hp;
    public float maxHp;

    public float stamina;
    public float maxStamina;

    public float attackPoint;
    public float defencePoint;

    public ItemData currentWeapon;
    public ItemData currentArmor;


    public float currentExp;
    public float maxExp;

    public float gold;
}

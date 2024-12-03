using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "Player / State")]
public class BasePlayerState : ScriptableObject
{
    public List<ItemData> items = new List<ItemData>();
    
    public int level;

    public int hp;
    public int maxHp;

    public int stamina;
    public int maxStamina;

    public int attackPoint;
    public int defencePoint;

    public ItemData currentWeapon;
    public ItemData currentArmor;


    public float currentExp;
    public float maxExp;

    public int gold;

    // JSON 직렬화
    public string ToJson()
    {
        return JsonUtility.ToJson(this, true);
    }

    // JSON 역직렬화
    public void LoadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}

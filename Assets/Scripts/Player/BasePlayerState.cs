using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "Player/State")]
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

    // JSON ����ȭ�� PlayerStateData Ŭ���� ����
    [System.Serializable]
    private class PlayerStateData
    {
        public int level;
        public int hp;
        public int maxHp;
        public int stamina;
        public int maxStamina;
        public int attackPoint;
        public int defencePoint;
        public int currentWeaponId;  // ���� ID ����
        public int currentArmorId;   // �� ID ����
        public float currentExp;
        public float maxExp;
        public int gold;
        public List<ItemInventoryData> items; // �κ��丮 ������

        [System.Serializable]
        public class ItemInventoryData
        {
            public int id;     // ������ ID
            public int value;  // ������ ����
        }
    }

    // JSON ����ȭ
    public string ToJson()
    {
        var data = new PlayerStateData
        {
            level = level,
            hp = hp,
            maxHp = maxHp,
            stamina = stamina,
            maxStamina = maxStamina,
            attackPoint = attackPoint,
            defencePoint = defencePoint,
            currentWeaponId = currentWeapon != null ? currentWeapon.id : -1,
            currentArmorId = currentArmor != null ? currentArmor.id : -1,
            currentExp = currentExp,
            maxExp = maxExp,
            gold = gold,
            items = items.ConvertAll(item => new PlayerStateData.ItemInventoryData
            {
                id = item.id,
                value = item.value
            })
        };

        return JsonUtility.ToJson(data, true);
    }

    // JSON ������ȭ
    public void LoadFromJson(string json)
    {
        var data = JsonUtility.FromJson<PlayerStateData>(json);

        // �Ϲ����� ���� �� ����
        level = data.level;
        hp = data.hp;
        maxHp = data.maxHp;
        stamina = data.stamina;
        maxStamina = data.maxStamina;
        attackPoint = data.attackPoint;
        defencePoint = data.defencePoint;
        currentExp = data.currentExp;
        maxExp = data.maxExp;
        gold = data.gold;

        // Resources���� ScriptableObject �ε�
        ItemData[] itemDatabase = Resources.LoadAll<ItemData>("Items");

        // ���� ���� ����� �� �ε�
        currentWeapon = Array.Find(itemDatabase, item => item.id == data.currentWeaponId);
        currentArmor = Array.Find(itemDatabase, item => item.id == data.currentArmorId);

        // �κ��丮 �ε�
        items = new List<ItemData>();
        foreach (var itemData in data.items)
        {
            // ������ ������ ID�� �´� �������� ã��
            var item = Array.Find(itemDatabase, i => i.id == itemData.id);
            if (item != null)
            {
                // �κ��丮 �������� value �� ���� (������ �ν��Ͻ��� �����ϰ� value�� ����)
                var itemInstance = Instantiate(item);
                itemInstance.value = itemData.value;
                items.Add(itemInstance);
            }
            else
            {
                Debug.LogWarning($"������ ID {itemData.id}�� �ش��ϴ� �������� ã�� �� �����ϴ�.");
            }
        }

        // �ε�� �������� ������ ��� �⺻ ������ �ʱ�ȭ�� �� �ֽ��ϴ�.
        if (currentWeapon == null)
        {
            Debug.LogWarning("������ ���⸦ ã�� �� ���� �⺻ ����� �����մϴ�.");
            // �⺻ ����� �����ϴ� ���� �߰� (��: �⺻ ���� 'defaultWeapon'�� ����)
        }
        if (currentArmor == null)
        {
            Debug.LogWarning("������ ���� ã�� �� ���� �⺻ ���� �����մϴ�.");
            // �⺻ ���� �����ϴ� ���� �߰�
        }
    }

}

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

    // JSON 직렬화용 PlayerStateData 클래스 정의
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
        public int currentWeaponId;  // 무기 ID 저장
        public int currentArmorId;   // 방어구 ID 저장
        public float currentExp;
        public float maxExp;
        public int gold;
        public List<ItemInventoryData> items; // 인벤토리 데이터

        [System.Serializable]
        public class ItemInventoryData
        {
            public int id;     // 아이템 ID
            public int value;  // 아이템 갯수
        }
    }

    // JSON 직렬화
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

    // JSON 역직렬화
    public void LoadFromJson(string json)
    {
        var data = JsonUtility.FromJson<PlayerStateData>(json);

        // 일반적인 상태 값 적용
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

        // Resources에서 ScriptableObject 로드
        ItemData[] itemDatabase = Resources.LoadAll<ItemData>("Items");

        // 장착 중인 무기와 방어구 로드
        currentWeapon = Array.Find(itemDatabase, item => item.id == data.currentWeaponId);
        currentArmor = Array.Find(itemDatabase, item => item.id == data.currentArmorId);

        // 인벤토리 로드
        items = new List<ItemData>();
        foreach (var itemData in data.items)
        {
            // 아이템 데이터 ID에 맞는 아이템을 찾음
            var item = Array.Find(itemDatabase, i => i.id == itemData.id);
            if (item != null)
            {
                // 인벤토리 아이템의 value 값 설정 (아이템 인스턴스를 생성하고 value를 설정)
                var itemInstance = Instantiate(item);
                itemInstance.value = itemData.value;
                items.Add(itemInstance);
            }
            else
            {
                Debug.LogWarning($"아이템 ID {itemData.id}에 해당하는 아이템을 찾을 수 없습니다.");
            }
        }

        // 로드된 아이템이 없으면 장비를 기본 값으로 초기화할 수 있습니다.
        if (currentWeapon == null)
        {
            Debug.LogWarning("장착된 무기를 찾을 수 없어 기본 무기로 설정합니다.");
            // 기본 무기로 설정하는 로직 추가 (예: 기본 무기 'defaultWeapon'을 설정)
        }
        if (currentArmor == null)
        {
            Debug.LogWarning("장착된 방어구를 찾을 수 없어 기본 방어구로 설정합니다.");
            // 기본 방어구로 설정하는 로직 추가
        }
    }

}

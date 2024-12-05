using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    WEAPON,
    ARMOR,
    POTION,
}
[CreateAssetMenu(fileName = "NewItemData", menuName = "Item/Item Data")]
[System.Serializable]
public class ItemData : ScriptableObject
{
    public string _name;              // 아이템 이름
    public string description;        // 아이템 설명
    public int id;                    // 고유 ID
    public ItemType type;             // 아이템 유형
    public int value;                 // 갯수
    public int rarerity;              // 희귀도
    public int price;                 // 가격
    public int status;                // 스텟

    // JSON 저장을 위한 필드 (Sprite는 JSON으로 저장 불가하므로 경로만 저장)
    public string iconPath;           // 아이콘 Sprite 경로
    public string bigImagePath;       // 큰 이미지 Sprite 경로


    [NonSerialized]
    public Sprite icon;               // 아이콘 (직렬화 대상 제외)
    [NonSerialized]
    public Sprite bigImage;           // 큰 이미지 (직렬화 대상 제외)

    public string ToJson()
    {
        return JsonUtility.ToJson(this, true);
    }

    public static ItemData FromJson(string json)
    {
        return JsonUtility.FromJson<ItemData>(json);
    }

    private void OnEnable()
    {
        // Resources 경로에서 스프라이트 로드
        if (!string.IsNullOrEmpty(iconPath))
        {
            icon = Resources.Load<Sprite>(iconPath);
            Debug.Log($"아이콘 로드 성공: {icon?.name}");
        }

        if (!string.IsNullOrEmpty(bigImagePath))
        {
            bigImage = Resources.Load<Sprite>(bigImagePath);
            Debug.Log($"큰 이미지 로드 성공: {bigImage?.name}");
        }
    }

}
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
    public string _name;              // ������ �̸�
    public string description;        // ������ ����
    public int id;                    // ���� ID
    public ItemType type;             // ������ ����
    public int value;                 // ����
    public int rarerity;              // ��͵�
    public int price;                 // ����
    public int status;                // ����

    // JSON ������ ���� �ʵ� (Sprite�� JSON���� ���� �Ұ��ϹǷ� ��θ� ����)
    public string iconPath;           // ������ Sprite ���
    public string bigImagePath;       // ū �̹��� Sprite ���


    [NonSerialized]
    public Sprite icon;               // ������ (����ȭ ��� ����)
    [NonSerialized]
    public Sprite bigImage;           // ū �̹��� (����ȭ ��� ����)

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
        // Resources ��ο��� ��������Ʈ �ε�
        if (!string.IsNullOrEmpty(iconPath))
        {
            icon = Resources.Load<Sprite>(iconPath);
            Debug.Log($"������ �ε� ����: {icon?.name}");
        }

        if (!string.IsNullOrEmpty(bigImagePath))
        {
            bigImage = Resources.Load<Sprite>(bigImagePath);
            Debug.Log($"ū �̹��� �ε� ����: {bigImage?.name}");
        }
    }

}
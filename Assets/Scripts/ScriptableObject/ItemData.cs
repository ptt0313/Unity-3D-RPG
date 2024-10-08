using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    WEAPON,
    ARMOR,
    POTION,
}
public class ItemData : ScriptableObject
{
    public GameObject prefab;
    public Vector3 position;
    public int id;
    public ItemType type;
    public string name;
    public string description;
    public int value;
    public Sprite icon;
    public Sprite bigImage;
    public int rarerity;
}
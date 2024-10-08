using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Armor Object", menuName = "Items / Armor")]

public class ArmorType : ItemData
{
    private void Awake()
    {
        type = ItemType.ARMOR;
    }
}

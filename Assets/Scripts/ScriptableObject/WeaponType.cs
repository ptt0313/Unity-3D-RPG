using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon Object", menuName = "Items / Weapon")]
public class WeaponType : ItemData
{
    private void Awake()
    {
        type = ItemType.WEAPON;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Potion Object", menuName = "Items / Potion")]
public class PotionType : ItemData
{
    private void Awake()
    {
        type = ItemType.POTION;
    }
}

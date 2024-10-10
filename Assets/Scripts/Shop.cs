using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    List<ItemData> shopItems = new List<ItemData>();
    public List<ItemInventoryUI> ItemInventoryUISlots;
    [SerializeField] ItemData armor;
    [SerializeField] ItemData weapon;
    [SerializeField] ItemData weapon2;
    [SerializeField] ItemData potion;

    private void Start()
    {
        shopItems.Add(armor);
        shopItems.Add(weapon);
        shopItems.Add(weapon2);
        shopItems.Add(potion);
    }
}

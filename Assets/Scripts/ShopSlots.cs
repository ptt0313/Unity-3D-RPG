using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlots : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public Image itemIconImage;
    public Image itemBigImage;
    public ItemData currentItemData;
    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI itemDescription;

    public void BuyItem()
    {
        if(GameManager.Instance.gold >= currentItemData.price)
        {
             GameManager.Instance.gold -= currentItemData.price;
            InventoryManager.Instance.Add(currentItemData);
            InventoryManager.Instance.ListItem();
        }
    }
}

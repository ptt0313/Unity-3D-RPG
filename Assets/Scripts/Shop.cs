using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    List<ItemData> shopItems = new List<ItemData>();
    public List<ShopSlots> shopSlots;
    [SerializeField] ItemData armor;
    [SerializeField] ItemData armor2;
    [SerializeField] ItemData weapon;
    [SerializeField] ItemData weapon2;
    [SerializeField] ItemData potion;
    public Transform itemContect;


    private void Start()
    {
        shopItems.Add(armor);
        shopItems.Add(armor2);
        shopItems.Add(weapon);
        shopItems.Add(weapon2);
        shopItems.Add(potion);
        
        ListItem();
    }
    public void ListItem()
    {
        foreach (Transform child in itemContect)
        {
            child.gameObject.SetActive(false);
            // 빈 슬롯 다 지우고
        }
        foreach (Transform child in itemContect)
        {
            if (!child.gameObject.activeSelf)
                //빈 슬롯 상태에서
            {
                for (int i = 0; i < shopItems.Count; i++)
                {
                    // 아이템 먹은 만큼 슬롯 활성화하고 UI 업데이트
                    shopSlots[i].gameObject.SetActive(true);
                    shopSlots[i].itemNameText.text = shopItems[i]._name;
                    shopSlots[i].itemIconImage.sprite = shopItems[i].icon;
                    shopSlots[i].itemBigImage.sprite = shopItems[i].bigImage;
                    shopSlots[i].itemPrice.text = $"{shopItems[i].price}";
                    shopSlots[i].itemDescription.text = shopItems[i].description;
                    shopSlots[i].currentItemData = shopItems[i];
                    // 슬롯에 커렌트 아이템을 넣어 이 아이템이 무엇인지 알게 해준다
                }
            }
        }
    }
}

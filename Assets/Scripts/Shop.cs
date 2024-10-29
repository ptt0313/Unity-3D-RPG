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
            // �� ���� �� �����
        }
        foreach (Transform child in itemContect)
        {
            if (!child.gameObject.activeSelf)
                //�� ���� ���¿���
            {
                for (int i = 0; i < shopItems.Count; i++)
                {
                    // ������ ���� ��ŭ ���� Ȱ��ȭ�ϰ� UI ������Ʈ
                    shopSlots[i].gameObject.SetActive(true);
                    shopSlots[i].itemNameText.text = shopItems[i]._name;
                    shopSlots[i].itemIconImage.sprite = shopItems[i].icon;
                    shopSlots[i].itemBigImage.sprite = shopItems[i].bigImage;
                    shopSlots[i].itemPrice.text = $"{shopItems[i].price}";
                    shopSlots[i].itemDescription.text = shopItems[i].description;
                    shopSlots[i].currentItemData = shopItems[i];
                    // ���Կ� Ŀ��Ʈ �������� �־� �� �������� �������� �˰� ���ش�
                }
            }
        }
    }
}

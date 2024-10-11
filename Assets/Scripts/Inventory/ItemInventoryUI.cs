using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInventoryUI : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public TextMeshProUGUI itemNameText;
    public Image itemIconImage;
    public Image itemBigImage;
    public TextMeshProUGUI countItemText;
    public ItemData currentItemData;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryManager.Instance.hilightItem.SetActive(true);
        InventoryManager.Instance.HilightItem(currentItemData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.Instance.hilightItem.SetActive(false);
    }
    public void OnMouseDown()
    {
        if (currentItemData.type == ItemType.POTION)
        {
            Debug.Log("���� �Դ´�");
            InventoryManager.Instance.Remove(currentItemData);
            // ������ �Һ������, ������ 0�̵Ǹ� �������
        }
        ChangeWeapon();
        // ����� ���� ��� �κ��丮�� �����鼭 ��ü
        Time.timeScale = 1.0f;
    }
    
    public void ChangeWeapon()
    {
        if (currentItemData == null)
        {
            return;
        }
        //FindObjectOfType<Player>().ActivateItem(currentItemData);
        InventoryManager.Instance.gameObject.SetActive(false);
        // �κ��丮���� �ش� ���⸦ ������ �װ����� ��ü�ϴ� �Լ�
    }

    
}

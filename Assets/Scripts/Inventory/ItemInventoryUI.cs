using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInventoryUI : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    public TextMeshProUGUI itemNameText;
    public Image itemIconImage;
    public Image itemBigImage;
    public TextMeshProUGUI countItemText;
    public ItemData currentItemData;
    private float currentWeaponStatus = 0f;
    private float currentArmorStatus = 0f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryManager.Instance.hilightItem.transform.position = eventData.position;
        InventoryManager.Instance.hilightItem.SetActive(true);
        InventoryManager.Instance.HilightItem(currentItemData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.Instance.hilightItem.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentItemData.type == ItemType.POTION)
        {
            Debug.Log("���� ���Ŵ�!");
            InventoryManager.Instance.Remove(currentItemData);
            // ������ �Һ������, ������ 0�̵Ǹ� �������
        }
        ChangeWeapon(eventData);
        // ����� ���� ��� �κ��丮�� �����鼭 ��ü
        Time.timeScale = 1.0f;
    }

    public void ChangeWeapon(PointerEventData eventData)
    {
        if(currentItemData.type == ItemType.WEAPON)
        {
            if(currentWeaponStatus == 0f)
            {
                PlayerInfomationManager.Instance.weaponEquipment.sprite = currentItemData.bigImage;
                PlayerInfomationManager.Instance.playerState.attackPoint += currentItemData.status;
                currentWeaponStatus = currentItemData.status;
            }
            else
            {
                PlayerInfomationManager.Instance.weaponEquipment.sprite = currentItemData.bigImage;
                PlayerInfomationManager.Instance.playerState.attackPoint += currentItemData.status;
                PlayerInfomationManager.Instance.playerState.attackPoint -= currentWeaponStatus;
                currentWeaponStatus = currentItemData.status;
            }
            Debug.Log("���� ����");
        }
        else if(currentItemData.type == ItemType.ARMOR)
        {
            if(currentArmorStatus == 0f)
            {
                PlayerInfomationManager.Instance.ArmorEquipment.sprite = currentItemData.bigImage;
                PlayerInfomationManager.Instance.playerState.defencePoint += currentItemData.status;
                currentArmorStatus = currentItemData.status;
            }
            else
            {
                PlayerInfomationManager.Instance.ArmorEquipment.sprite = currentItemData.bigImage;
                PlayerInfomationManager.Instance.playerState.defencePoint += currentItemData.status;
                PlayerInfomationManager.Instance.playerState.defencePoint -= currentArmorStatus;
                currentArmorStatus = currentItemData.status;
            }
            Debug.Log("�� ����");
        }
        if (currentItemData == null)
        {
            return;
        }
        // �κ��丮���� �ش� ��� ������ ����
    }

}

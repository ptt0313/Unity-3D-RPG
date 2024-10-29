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
            PlayerInfomationManager.Instance.playerState.hp += 50;
            if(PlayerInfomationManager.Instance.playerState.hp >= PlayerInfomationManager.Instance.playerState.maxHp)
            {
                PlayerInfomationManager.Instance.playerState.hp = PlayerInfomationManager.Instance.playerState.maxHp;
            }
            // ������ �Һ������, ������ 0�̵Ǹ� �������
        }
        ChangeWeapon(eventData);
        // ����� ���� ��� �κ��丮�� �����鼭 ��ü
        Time.timeScale = 1.0f;
    }

    public void ChangeWeapon(PointerEventData eventData)
    {
        if (currentItemData == null)
        {
            return;
        }
        if (currentItemData.type == ItemType.WEAPON)
        {
            // ���� ����
            if(PlayerInfomationManager.Instance.playerState.currentWeapon == currentItemData)
            {
                PlayerInfomationManager.Instance.playerState.currentWeapon = null;
                PlayerInfomationManager.Instance.weaponEquipment.sprite = null;
                PlayerInfomationManager.Instance.playerState.attackPoint -= currentItemData.status;
            }
            // �������� ��� ������ ��� ����
            else if (PlayerInfomationManager.Instance.playerState.currentWeapon == null)
            {
                PlayerInfomationManager.Instance.playerState.currentWeapon = currentItemData;
                PlayerInfomationManager.Instance.weaponEquipment.sprite = currentItemData.bigImage;
                PlayerInfomationManager.Instance.playerState.attackPoint += currentItemData.status;
            }
            // �������� ��� ������ ��� ��ü
            else
            {
                PlayerInfomationManager.Instance.playerState.attackPoint -= PlayerInfomationManager.Instance.playerState.currentWeapon.status;
                PlayerInfomationManager.Instance.playerState.currentWeapon = currentItemData;
                PlayerInfomationManager.Instance.weaponEquipment.sprite = currentItemData.bigImage;
                PlayerInfomationManager.Instance.playerState.attackPoint += currentItemData.status;
            }
            Debug.Log("���� ����");
        }
        else if(currentItemData.type == ItemType.ARMOR)
        {
            // ���� ����
            if(PlayerInfomationManager.Instance.playerState.currentArmor == currentItemData)
            {
                PlayerInfomationManager.Instance.playerState.currentArmor = null;
                PlayerInfomationManager.Instance.armorEquipment.sprite = null;
                PlayerInfomationManager.Instance.playerState.defencePoint -= currentItemData.status;
            }
            // �������� ��� ������ ��� ����
            else if(PlayerInfomationManager.Instance.playerState.currentArmor == null)
            {
                PlayerInfomationManager.Instance.playerState.currentArmor = currentItemData;
                PlayerInfomationManager.Instance.armorEquipment.sprite = currentItemData.bigImage;
                PlayerInfomationManager.Instance.playerState.defencePoint += currentItemData.status;
            }
            // �������� ��� ������ ��� ��ü
            else
            {
                PlayerInfomationManager.Instance.playerState.defencePoint -= PlayerInfomationManager.Instance.playerState.currentArmor.status;
                PlayerInfomationManager.Instance.playerState.currentArmor = currentItemData;
                PlayerInfomationManager.Instance.armorEquipment.sprite = currentItemData.bigImage;
                PlayerInfomationManager.Instance.playerState.defencePoint += currentItemData.status;
            }
            Debug.Log("�� ����");
        }

        PlayerInfomationManager.Instance.UpdateStat();

        
        // �κ��丮���� �ش� ��� ������ ����
    }

}

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
            Debug.Log("포션 마신다!");
            InventoryManager.Instance.Remove(currentItemData);
            PlayerInfomationManager.Instance.playerState.hp += 50;
            if(PlayerInfomationManager.Instance.playerState.hp >= PlayerInfomationManager.Instance.playerState.maxHp)
            {
                PlayerInfomationManager.Instance.playerState.hp = PlayerInfomationManager.Instance.playerState.maxHp;
            }
            // 포션은 소비아이템, 갯수가 0이되면 사라진다
        }
        ChangeWeapon(eventData);
        // 무기와 방어구는 계속 인벤토리에 있으면서 교체
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
            // 장착 해제
            if(PlayerInfomationManager.Instance.playerState.currentWeapon == currentItemData)
            {
                PlayerInfomationManager.Instance.playerState.currentWeapon = null;
                PlayerInfomationManager.Instance.weaponEquipment.sprite = null;
                PlayerInfomationManager.Instance.playerState.attackPoint -= currentItemData.status;
            }
            // 장착중인 장비가 없을때 장비 장착
            else if (PlayerInfomationManager.Instance.playerState.currentWeapon == null)
            {
                PlayerInfomationManager.Instance.playerState.currentWeapon = currentItemData;
                PlayerInfomationManager.Instance.weaponEquipment.sprite = currentItemData.bigImage;
                PlayerInfomationManager.Instance.playerState.attackPoint += currentItemData.status;
            }
            // 장착중인 장비가 있을때 장비 교체
            else
            {
                PlayerInfomationManager.Instance.playerState.attackPoint -= PlayerInfomationManager.Instance.playerState.currentWeapon.status;
                PlayerInfomationManager.Instance.playerState.currentWeapon = currentItemData;
                PlayerInfomationManager.Instance.weaponEquipment.sprite = currentItemData.bigImage;
                PlayerInfomationManager.Instance.playerState.attackPoint += currentItemData.status;
            }
            Debug.Log("무기 장착");
        }
        else if(currentItemData.type == ItemType.ARMOR)
        {
            // 장착 해제
            if(PlayerInfomationManager.Instance.playerState.currentArmor == currentItemData)
            {
                PlayerInfomationManager.Instance.playerState.currentArmor = null;
                PlayerInfomationManager.Instance.armorEquipment.sprite = null;
                PlayerInfomationManager.Instance.playerState.defencePoint -= currentItemData.status;
            }
            // 장착중인 장비가 없을때 장비 장착
            else if(PlayerInfomationManager.Instance.playerState.currentArmor == null)
            {
                PlayerInfomationManager.Instance.playerState.currentArmor = currentItemData;
                PlayerInfomationManager.Instance.armorEquipment.sprite = currentItemData.bigImage;
                PlayerInfomationManager.Instance.playerState.defencePoint += currentItemData.status;
            }
            // 장착중인 장비가 있을때 장비 교체
            else
            {
                PlayerInfomationManager.Instance.playerState.defencePoint -= PlayerInfomationManager.Instance.playerState.currentArmor.status;
                PlayerInfomationManager.Instance.playerState.currentArmor = currentItemData;
                PlayerInfomationManager.Instance.armorEquipment.sprite = currentItemData.bigImage;
                PlayerInfomationManager.Instance.playerState.defencePoint += currentItemData.status;
            }
            Debug.Log("방어구 장착");
        }

        PlayerInfomationManager.Instance.UpdateStat();

        
        // 인벤토리에서 해당 장비를 누르면 장착
    }

}

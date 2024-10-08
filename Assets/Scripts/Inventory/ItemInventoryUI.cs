using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInventoryUI : MonoBehaviour, IPointerEnterHandler
{
    public TextMeshProUGUI itemIconNameText;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public Image itemIconImage;
    public Image itemBigImage;
    public TextMeshProUGUI countitemText;
    public ItemData currentItemData;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //HilightItem();
    }
    public void OnMouseDown()
    {
        if (currentItemData.type == ItemType.POTION)
        {
            Debug.Log("포션 먹는다");
            InventoryManager.inventoryManager.Remove(currentItemData);
            // 포션은 소비아이템, 갯수가 0이되면 사라진다
        }
        ChangeWeapon();
        // 무기와 방어구는 계속 인벤토리에 있으면서 교체
        Time.timeScale = 1.0f;
    }
    
    public void ChangeWeapon()
    {
        if (currentItemData == null)
        {
            return;
        }
        //FindObjectOfType<Player>().ActivateItem(currentItemData);
        InventoryManager.inventoryManager.gameObject.SetActive(false);
        // 인벤토리에서 해당 무기를 누르면 그것으로 교체하는 함수
    }
}

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
            Debug.Log("���� �Դ´�");
            InventoryManager.inventoryManager.Remove(currentItemData);
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
        InventoryManager.inventoryManager.gameObject.SetActive(false);
        // �κ��丮���� �ش� ���⸦ ������ �װ����� ��ü�ϴ� �Լ�
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class InventoryManager : Singleton<InventoryManager>
{
    public List<ItemData> items = new List<ItemData>();
    int maxInventorySlots = 16;
    [SerializeField] public GameObject inventory;
    public Transform itemContect;
    public List<ItemInventoryUI> ItemInventoryUISlots;
    public delegate void OnItemChanged();
    public static event OnItemChanged onItemChagedCallback;
    [SerializeField] public GameObject hilightItem;
    [SerializeField] Image hilightItemImage;
    [SerializeField] TextMeshProUGUI hilightItemName;
    [SerializeField] TextMeshProUGUI hilightItemDescription;

    private void Start()
    {
        ListItem();
        // 시작 할 때 아이템이 있으면 인벤토리 UI 업데이트 
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool isActive = !inventory.activeSelf;
            inventory.SetActive(isActive); // 인벤토리 UI 활성화/비활성화 토글
                                           // 인벤토리가 활성화되면 마우스 커서를 표시하고, 그렇지 않으면 숨깁니다.
            Cursor.visible = isActive;
            // 인벤토리가 활성화되면 마우스 커서를 잠그지 않고, 그렇지 않으면 잠급니다.
            Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
    public void Add(ItemData newItem)
    {
        ItemData existingItem = items.Find(item => item._name == newItem._name);
        if (existingItem != null)
        {
            existingItem.value += 1;
            // 같은 아이템이면 카운트 +1
        }

        else
        {
            newItem.value = 1;
            items.Add(newItem);
            // 새로운 아이템이면 추가
        }
        onItemChagedCallback?.Invoke(); // 아이템 변경 이벤트 발생
    }

    public void Remove(ItemData item)
    {
        ItemData itemToRemove = items.Find(i => i._name == item._name);
        if (itemToRemove != null && itemToRemove.value > 0)
        {
            itemToRemove.value -= 1;
            int index = items.IndexOf(itemToRemove);
            ItemInventoryUISlots[index].countItemText.text = itemToRemove.value.ToString();
            Debug.Log("포션이 깎여야함");

            if (itemToRemove.value == 0)
            {
                Debug.Log("포션이 0이 되어야 함");
                ItemInventoryUISlots[index].gameObject.SetActive(false);
                items.Remove(itemToRemove);
            }

            onItemChagedCallback?.Invoke();
        }
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
            // 빈 슬롯 상태에서
            {
                for (int i = 0; i < items.Count; i++)
                {
                    // 아이템 먹은 만큼 슬롯 활성화하고 UI 업데이트
                    ItemInventoryUISlots[i].gameObject.SetActive(true);
                    ItemInventoryUISlots[i].itemNameText.text = items[i]._name;
                    ItemInventoryUISlots[i].itemIconImage.sprite = items[i].icon;
                    ItemInventoryUISlots[i].itemBigImage.sprite = items[i].bigImage;
                    ItemInventoryUISlots[i].countItemText.text = $"{items[i].value}";
                    ItemInventoryUISlots[i].currentItemData = items[i];
                    // 슬롯에 커렌트 아이템을 넣어 이 아이템이 무엇인지 알게 해준다
                }
            }
        }
    }
    public void HilightItem(ItemData itemData)
    {
        hilightItemImage.sprite = itemData.bigImage;
        hilightItemDescription.text = itemData.description;
        hilightItemName.text = itemData._name;
    }
}

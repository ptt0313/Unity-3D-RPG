using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class InventoryManager : Singleton<InventoryManager>
{
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
        // ���� �� �� �������� ������ �κ��丮 UI ������Ʈ 
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool isActive = !inventory.activeSelf;
            inventory.SetActive(isActive); // �κ��丮 UI Ȱ��ȭ/��Ȱ��ȭ ���
                                           // �κ��丮�� Ȱ��ȭ�Ǹ� ���콺 Ŀ���� ǥ���ϰ�, �׷��� ������ ����ϴ�.
            Cursor.visible = isActive;
            // �κ��丮�� Ȱ��ȭ�Ǹ� ���콺 Ŀ���� ����� �ʰ�, �׷��� ������ ��޴ϴ�.
            Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
        }
        if(inventory.activeSelf == false)
        {
            hilightItem.SetActive(false);
        }
    }
    public void Add(ItemData newItem)
    {
        ItemData existingItem = PlayerInfomationManager.Instance.playerState.items.Find(item => item._name == newItem._name);
        if (existingItem != null)
        {
            existingItem.value += 1;
            // ���� �������̸� ī��Ʈ +1
        }
        else
        {
            newItem.value = 1;
            PlayerInfomationManager.Instance.playerState.items.Add(newItem);
            // ���ο� �������̸� �߰�
        }
        onItemChagedCallback?.Invoke(); // ������ ���� �̺�Ʈ �߻�
    }

    public void Remove(ItemData item)
    {
        ItemData itemToRemove = PlayerInfomationManager.Instance.playerState.items.Find(i => i._name == item._name);
        if (itemToRemove != null && itemToRemove.value > 0)
        {
            itemToRemove.value -= 1;
            int index = PlayerInfomationManager.Instance.playerState.items.IndexOf(itemToRemove);
            ItemInventoryUISlots[index].countItemText.text = itemToRemove.value.ToString();
            Debug.Log("������ �𿩾���");

            if (itemToRemove.value == 0)
            {
                Debug.Log("������ 0�� �Ǿ�� ��");
                ItemInventoryUISlots[index].gameObject.SetActive(false);
                PlayerInfomationManager.Instance.playerState.items.Remove(itemToRemove);
                hilightItem.SetActive(false);
            }

            onItemChagedCallback?.Invoke();
        }
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
            // �� ���� ���¿���
            {
                for (int i = 0; i < PlayerInfomationManager.Instance.playerState.items.Count; i++)
                {
                    // ������ ���� ��ŭ ���� Ȱ��ȭ�ϰ� UI ������Ʈ
                    ItemInventoryUISlots[i].gameObject.SetActive(true);
                    ItemInventoryUISlots[i].itemNameText.text = PlayerInfomationManager.Instance.playerState.items[i]._name;
                    ItemInventoryUISlots[i].itemIconImage.sprite = PlayerInfomationManager.Instance.playerState.items[i].icon;
                    ItemInventoryUISlots[i].itemBigImage.sprite = PlayerInfomationManager.Instance.playerState.items[i].bigImage;
                    ItemInventoryUISlots[i].countItemText.text = $"{PlayerInfomationManager.Instance.playerState.items[i].value}";
                    ItemInventoryUISlots[i].currentItemData = PlayerInfomationManager.Instance.playerState.items[i];
                    // ���Կ� Ŀ��Ʈ �������� �־� �� �������� �������� �˰� ���ش�
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

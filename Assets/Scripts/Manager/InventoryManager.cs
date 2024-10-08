using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager inventoryManager;
    public List<ItemData> items = new List<ItemData>();
    int maxInventorySlots = 16;
    [SerializeField] public GameObject inventory;
    public Transform itemContect;
    public List<ItemInventoryUI> ItemInventoryUISlots;
    public delegate void OnItemChanged();
    public static event OnItemChanged onItemChagedCallback;
    private void Awake()
    {
        if (inventoryManager != null && inventoryManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            inventoryManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
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
            UnityEngine.Cursor.visible = isActive;
            // �κ��丮�� Ȱ��ȭ�Ǹ� ���콺 Ŀ���� ����� �ʰ�, �׷��� ������ ��޴ϴ�.
            UnityEngine.Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
    public void Add(ItemData newItem)
    {
        ItemData existingItem = items.Find(item => item.name == newItem.name);
        if (existingItem != null)
        {
            existingItem.value += 1;
            // ���� �������̸� ī��Ʈ +1
        }

        else
        {
            newItem.value = 1;
            items.Add(newItem);
            // ���ο� �������̸� �߰�
        }
        onItemChagedCallback?.Invoke(); // ������ ���� �̺�Ʈ �߻�
    }

    public void Remove(ItemData item)
    {
        ItemData itemToRemove = items.Find(i => i.name == item.name);
        if (itemToRemove != null && itemToRemove.value > 0)
        {
            itemToRemove.value -= 1;
            int index = items.IndexOf(itemToRemove);
            ItemInventoryUISlots[index].countitemText.text = itemToRemove.value.ToString();
            Debug.Log("������ �𿩾���");

            if (itemToRemove.value == 0)
            {
                Debug.Log("������ 0�� �Ǿ�� ��");
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
            // �� ���� �� �����
        }
        foreach (Transform child in itemContect)
        {
            if (!child.gameObject.activeSelf)
            // �� ���� ���¿���
            {
                for (int i = 0; i < items.Count; i++)
                {
                    // ������ ���� ��ŭ ���� Ȱ��ȭ�ϰ� UI ������Ʈ
                    ItemInventoryUISlots[i].gameObject.SetActive(true);
                    ItemInventoryUISlots[i].itemIconNameText.text = items[i].name;
                    ItemInventoryUISlots[i].itemNameText.text = items[i].name;
                    ItemInventoryUISlots[i].itemDescriptionText.text = items[i].description;
                    ItemInventoryUISlots[i].itemIconImage.sprite = items[i].icon;
                    ItemInventoryUISlots[i].itemBigImage.sprite = items[i].bigImage;
                    ItemInventoryUISlots[i].countitemText.text = $"{items[i].value}";
                    ItemInventoryUISlots[i].currentItemData = items[i];
                    // ���Կ� Ŀ��Ʈ �������� �־� �� �������� �������� �˰� ���ش�
                }
            }
        }
    }
}

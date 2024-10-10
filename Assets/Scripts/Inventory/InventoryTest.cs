using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    [SerializeField] ItemData itemData;
    
    public void AddItem()
    {
        InventoryManager.Instance.Add(itemData);
        InventoryManager.Instance.ListItem();
    }
}

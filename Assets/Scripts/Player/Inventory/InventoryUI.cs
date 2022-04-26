using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    private List<InventorySlot> inventorySlots = new List<InventorySlot>(8);

    public void ResetInventory()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        inventorySlots = new List<InventorySlot>(8);
    }

    public void DrawInventory(List<InventoryItem> inventoryItems)
    {
        ResetInventory();
        for (int i = 0; i < inventorySlots.Capacity; i++)
        {
            CreateInventorySlot();
        }

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            inventorySlots[i].DrawSlot(inventoryItems[i]);
        }
    }

    private void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform,false);

        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();
        
        inventorySlots.Add(newSlotComponent);
        
    }
    
    
}
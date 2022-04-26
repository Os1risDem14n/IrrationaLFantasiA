using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public event EventHandler OnInventoryItemListChanged;

    private List<InventoryInventoryItem> InventoryItemList;
    private Action<InventoryInventoryItem> useInventoryItemAction;

    public Inventory(Action<InventoryItem> useInventoryItemAction)
    {
        this.useInventoryItemAction = useInventoryItemAction;
        InventoryItemList = new List<InventoryItem>();

        AddInventoryItem(new InventoryItem {InventoryItemType = InventoryItem.InventoryItemType.Sword, amount = 1});
        AddInventoryItem(new InventoryItem {InventoryItemType = InventoryItem.InventoryItemType.HealthPotion, amount = 1});
        AddInventoryItem(new InventoryItem {InventoryItemType = InventoryItem.InventoryItemType.ManaPotion, amount = 1});
    }

    public void AddInventoryItem(InventoryItem InventoryItem)
    {
        if (InventoryItem.IsStackable())
        {
            bool InventoryItemAlreadyInInventory = false;
            foreach (InventoryItem inventoryInventoryItem in InventoryItemList)
            {
                if (inventoryInventoryItem.InventoryItemType == InventoryItem.InventoryItemType)
                {
                    inventoryInventoryItem.amount += InventoryItem.amount;
                    InventoryItemAlreadyInInventory = true;
                }
            }

            if (!InventoryItemAlreadyInInventory)
            {
                InventoryItemList.Add(InventoryItem);
            }
        }
        else
        {
            InventoryItemList.Add(InventoryItem);
        }

        OnInventoryItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveInventoryItem(InventoryItem InventoryItem)
    {
        if (InventoryItem.IsStackable())
        {
            InventoryItem InventoryItemInInventory = null;
            foreach (InventoryItem inventoryInventoryItem in InventoryItemList)
            {
                if (inventoryInventoryItem.InventoryItemType == InventoryItem.InventoryItemType)
                {
                    inventoryInventoryItem.amount -= InventoryItem.amount;
                    InventoryItemInInventory = inventoryInventoryItem;
                }
            }

            if (InventoryItemInInventory != null && InventoryItemInInventory.amount <= 0)
            {
                InventoryItemList.Remove(InventoryItemInInventory);
            }
        }
        else
        {
            InventoryItemList.Remove(InventoryItem);
        }

        OnInventoryItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseInventoryItem(InventoryItem InventoryItem)
    {
        useInventoryItemAction(InventoryItem);
    }

    public List<InventoryItem> GetInventoryItemList()
    {
        return InventoryItemList;
    }
}
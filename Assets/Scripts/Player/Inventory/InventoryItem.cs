using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
[System.Serializable]
public class InventoryItem : MonoBehaviour
{
    public enum InventoryItemType {
        Eye,
        Fire,
        Heart,
        Moon,
        Skull,
        Sun,
        Tree,
        Water,
        Wind
    }

    public InventoryItemType inventoryItem;
    public string itemName;
    public Sprite itemImage;
    public string itemDetail;
    public int itemPrice;
}

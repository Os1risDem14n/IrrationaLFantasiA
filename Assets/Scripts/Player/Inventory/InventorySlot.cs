using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image slotIcon;
    public TMP_Text slotButton;

    public void ClearSlot()
    {
        slotIcon.enabled = false;
        slotButton.enabled = false;
    }

    public void DrawSlot(InventoryItem item)
    {
        if (item == null)
        {
            ClearSlot();
            return;
        }
        
        slotIcon.enabled = true;
        slotButton.enabled = true;
        slotIcon.sprite = item.itemImage;
        slotButton.text = item.itemName;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public int slotIndex;
    [SerializeField] private Item itemProfile;
    [SerializeField] private int itemCount;
    public Item ItemProfile { get { return itemProfile; } private set { itemProfile = value; } }
    public int ItemCount { get { return itemCount; } private set { itemCount = value; } }
    
    public void SetItem(Item item, int count)
    {
        if (item == null)
        {
            ClearItem();
            return;
        }
        itemProfile = item;
        itemCount = count;
        GetComponentInChildren<DropSlot>().currentItem.SetUI(itemProfile, itemCount);
    }

    public void ClearItem()
    {
        itemProfile = null;
        itemCount = 0;
        GetComponentInChildren<DropSlot>().currentItem.ClearUI();
    }
    
    
}

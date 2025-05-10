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
    public bool onEquip;
    [SerializeField] private Item itemProfile;
    [SerializeField] private int itemCount;
    public Item ItemProfile { get { return itemProfile; } private set { itemProfile = value; } }
    public int ItemCount { get { return itemCount; } private set { itemCount = value; } }
    public void SetItem(Item item, int count,bool isEquipped)
    {
        if (item == null)
        {
            ClearItem();
            return;
        }
        itemProfile = item;
        itemCount = count;
        onEquip = isEquipped;
        GetComponentInChildren<DropSlot>().currentItem.SetUI(itemProfile, itemCount, onEquip);
    }

    public void ClearItem()
    {
        itemProfile = null;
        itemCount = 0;
        GetComponentInChildren<DropSlot>().currentItem.ClearUI();
    }
    
    
}

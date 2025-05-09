using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    
    public static InventoryManager Instance { get; private set; }

    public static Action<List<InventorySlot>> OnInventoryChanged;
    
    [Header("Inventory Setting")]
    public int slotCount = 10;
    public List<InventorySlot> inventorySlots = new();
    
    [Header("Inventory UI Setup")]
    public GameObject itemSlotPrefab;
    public Transform inventorySlotsParent;
    public List<ItemSlotUI> slotsUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        SetUpInventory();
    }

    private void SetUpInventory()
    {
        for (int i = 0; i < slotCount; i++)
            inventorySlots.Add(new InventorySlot());
        for (int i = 0; i < slotCount; i++)
        {
            ItemSlotUI slotUI = Instantiate(itemSlotPrefab, inventorySlotsParent).GetComponent<ItemSlotUI>();
            slotsUI.Add(slotUI);
        }
    }

    private void SetUpItemSlotUI()
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (slotsUI[i].ItemProfile == inventorySlots[i].item)
            {
                continue;
            }
            slotsUI[i].SetItem(inventorySlots[i].item);
        }
    }

    public void AddItem(Item item)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.CanStack(item))
            {
                slot.Add(item);
                OnInventoryChanged?.Invoke(inventorySlots);
                return;
            }
        }

        foreach (var slot in inventorySlots)
        {
            if (slot.IsEmpty)
            {
                slot.Add(item);
                OnInventoryChanged?.Invoke(inventorySlots);
                SetUpItemSlotUI();
                return;
            }
        }
        Debug.Log("Inventory Full!");
    }

    public bool CheckInventoryFull()
    {
        bool isFull = true;
        foreach (var slot in inventorySlots)
        {
            if (slot.IsEmpty)
            {
                isFull = false;
            }
        }

        return isFull;
    }

    public void RemoveItem(Item item)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.Contains(item))
            {
                slot.Remove(item);
                OnInventoryChanged?.Invoke(inventorySlots);
                SetUpItemSlotUI();
                return;
            }
        }
    }
    
    [ContextMenu("Sort by Name")]
    public void SortByName()
    {
        inventorySlots.Sort((a, b) => a.item.itemName.CompareTo(b.item.itemName));
        SetUpItemSlotUI();
    }
    [ContextMenu("Sort by Number")]
    public void SortByNumber()
    {
        inventorySlots.Sort((a, b) => a.quantity.CompareTo(b.quantity));
        SetUpItemSlotUI();
    }
    [ContextMenu("Sort by Type")]
    public void SortByType()
    {
        inventorySlots.Sort((a, b) => a.item.itemType.CompareTo(b.item.itemType));
        SetUpItemSlotUI();
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public static Action<List<InventorySlot>> OnInventoryChanged;

    public int slotCount = 10;
    public List<InventorySlot> inventorySlots = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Initialize empty slots
        for (int i = 0; i < slotCount; i++)
            inventorySlots.Add(new InventorySlot());
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
                return;
            }
        }
    }
}
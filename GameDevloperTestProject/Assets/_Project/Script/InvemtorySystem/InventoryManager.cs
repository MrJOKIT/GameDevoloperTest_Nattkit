using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Player player;
    public static InventoryManager Instance { get; private set; }

    public static Action<List<InventorySlot>> OnInventoryChanged;
    
    [Header("Inventory Setting")]
    public int slotCount = 10;
    public List<InventorySlot> inventorySlots = new();
    
    [Header("Inventory UI Setup")]
    public GameObject itemSlotPrefab;
    public Transform inventorySlotsParent;
    public List<ItemSlotUI> slotsUI;
    
    [Header("Inventory Equipment Setup")]
    public Item equipmentSlot;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        SetUpInventory();
    }
    
    #region Inventory

    private void SetUpInventory()
    {
        for (int i = 0; i < slotCount; i++)
            inventorySlots.Add(new InventorySlot());
        for (int i = 0; i < slotCount; i++)
        {
            ItemSlotUI slotUI = Instantiate(itemSlotPrefab, inventorySlotsParent).GetComponent<ItemSlotUI>();
            slotsUI.Add(slotUI);
            slotsUI[i].slotIndex = i;
        }
    }

    [ContextMenu("Set Inventory UI Setup")]
    private void SetUpItemSlotUI()
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (slotsUI[i].ItemProfile == inventorySlots[i].item && slotsUI[i].ItemCount == inventorySlots[i].quantity && slotsUI[i].onEquip == inventorySlots[i].onEquip)
            {
                continue;
            }
            slotsUI[i].SetItem(inventorySlots[i].item,inventorySlots[i].quantity,inventorySlots[i].onEquip);
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
                SetUpItemSlotUI();
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

    public bool CheckInventoryFull(Item item)
    {
        bool isFull = true;
        foreach (var slot in inventorySlots)
        {
            if (slot.IsEmpty || slot.CanStack(item))
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
    #endregion

    #region Inventory Equip

    public void UseItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= inventorySlots.Count)
            return;

        var slot = inventorySlots[slotIndex];
        if (slot.item == null)
            return;

        Item item = slot.item;

        switch (item.itemType)
        {
            case ItemType.Tool:
                EquipItem(slotIndex);
                break;

            case ItemType.Seed:
                Debug.Log("🌱 เตรียมปลูก: " + item.itemName);
                // TODO: เตรียมระบบปลูก
                break;

            case ItemType.CraftedObject:
                Debug.Log("🧪 ใช้: " + item.itemName);
                slot.Remove(item); // ลดจำนวนหรือเอาออก
                break;

            case ItemType.Resources:
                Debug.Log("📦 ไม่สามารถใช้ได้โดยตรง: " + item.itemName);
                break;
        }

        SetUpItemSlotUI();
        OnInventoryChanged?.Invoke(inventorySlots);
    }
    private void EquipItem(int slotIndex)
    {
        Item tempItem = inventorySlots[slotIndex].item;
        if (tempItem != equipmentSlot)
        {
            if (equipmentSlot != null)
            {
                int index = inventorySlots.FindIndex(slot => slot.item == equipmentSlot);
                inventorySlots[index].onEquip = false;
            }
            inventorySlots[slotIndex].onEquip = true;
            equipmentSlot = tempItem;
            SetUpItemSlotUI();
        }
    }

    #endregion
    
    #region Inventory Sorting
    [ContextMenu("Sort by Name")]
    public void SortByName()
    {
        inventorySlots.Sort((a, b) =>
        {
            string aName = a.item?.itemName;
            string bName = b.item?.itemName;

            if (aName == null && bName != null) return 1;
            if (bName == null && aName != null) return -1;    
            if (aName == null && bName == null) return 0;    

            return aName.CompareTo(bName);                    
        });

        SetUpItemSlotUI();
    }
    [ContextMenu("Sort by Number")]
    public void SortByNumber()
    {
        inventorySlots.Sort((a, b) =>
        {
            if (a.quantity == 0 && b.quantity != 0) return 1;
            if (b.quantity == 0 && a.quantity != 0) return -1;
            return a.quantity.CompareTo(b.quantity);
        });
        SetUpItemSlotUI();
    }
    [ContextMenu("Sort by Type")]
    public void SortByType()
    {
        inventorySlots.Sort((a, b) =>
        {
            bool aNull = a.item == null;
            bool bNull = b.item == null;

            if (aNull && !bNull) return 1;
            if (bNull && !aNull) return -1;
            if (aNull && bNull) return 0;

            return a.item.itemType.CompareTo(b.item.itemType);
        });
        SetUpItemSlotUI();
    }
    
    public static void Swap<T>(List<T> list, int indexA, int indexB)
    {
        if (indexA < 0 || indexB < 0 || indexA >= list.Count || indexB >= list.Count)
        {
            Debug.LogWarning("Invalid index for swap.");
            return;
        }

        T temp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = temp;
    }

    public void SwapSlot(int indexA, int indexB)
    {
        Swap(inventorySlots,indexA,indexB);
        SetUpItemSlotUI();
    }
    #endregion
}

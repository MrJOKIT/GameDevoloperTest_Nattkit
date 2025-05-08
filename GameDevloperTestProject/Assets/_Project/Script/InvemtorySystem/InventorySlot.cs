
using System;

[Serializable]
public class InventorySlot
{
    public Item item;
    public int quantity;

    public bool IsEmpty => item == null;

    public bool CanStack(Item newItem)
    {
        return item != null && item.itemName == newItem.itemName && quantity < item.maxStack;
    }

    public void Add(Item newItem)
    {
        if (IsEmpty)
        {
            item = newItem;
            quantity = 1;
        }
        else
        {
            quantity++;
        }
    }

    public void Remove(Item targetItem)
    {
        if (item != null && item.itemName == targetItem.itemName)
        {
            quantity--;
            if (quantity <= 0)
                item = null;
        }
    }

    public bool Contains(Item targetItem)
    {
        return item != null && item.itemName == targetItem.itemName;
    }
}
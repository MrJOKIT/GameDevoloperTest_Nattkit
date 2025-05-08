using UnityEngine;

public enum ItemType
{
    Lumber, Seed, Tool, CraftedObject
}

public abstract class Item
{
    public string itemName;
    public ItemType itemType;
    public int maxStack = 10;

    public IItemUseStrategy useStrategy;

    public void Use()
    {
        useStrategy?.Use(this);
    }
}
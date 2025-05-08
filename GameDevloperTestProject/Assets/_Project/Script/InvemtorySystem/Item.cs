using UnityEngine;

public enum ItemType
{
    Resources, Seed, Tool, CraftedObject
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("UI")]
    public Sprite itemIcon;
    public string itemName;
    [Header("State")]
    public ItemType itemType;
    public int maxStack = 10;
    
}
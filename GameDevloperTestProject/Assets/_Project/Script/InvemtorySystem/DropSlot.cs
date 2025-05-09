using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public ItemSlotUI currentSlot;
    public DraggableItem currentItem;
    
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag; 
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        int slotA = draggableItem.parentAfterDrag.GetComponent<DropSlot>().currentSlot.slotIndex;
        int slotB = currentSlot.slotIndex;
        //swap to draggableItem
        currentItem.parentAfterDrag = draggableItem.parentAfterDrag;
        currentItem.transform.SetParent(currentItem.parentAfterDrag);
        
        draggableItem.parentAfterDrag.GetComponent<DropSlot>().currentItem = currentItem;
        
        //swap to this
        currentItem = draggableItem;
        draggableItem.parentAfterDrag = transform;
        draggableItem.transform.SetParent(transform);
        
        Debug.Log($"Swap item {slotA} to {slotB} ");
        InventoryManager.Instance.SwapSlot(slotA, slotB);
    }
}

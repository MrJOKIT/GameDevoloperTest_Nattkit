using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public DraggableItem currentItem;
    
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        
        //swap to draggableItem
        currentItem.parentAfterDrag = draggableItem.parentAfterDrag;
        currentItem.transform.SetParent(currentItem.parentAfterDrag);
        draggableItem.parentAfterDrag.GetComponent<DropSlot>().currentItem = currentItem;
        
        //swap to this
        draggableItem.parentAfterDrag = transform;
        currentItem = draggableItem;
    }
}

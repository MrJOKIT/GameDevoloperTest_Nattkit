using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler ,IPointerEnterHandler,IPointerExitHandler
{
    public Image currentSlot;
    public Transform parentAfterDrag;
    
    [Space(10)]  
    [Header("Item Slot UI")] 
    [SerializeField] private GameObject itemInfoCanvas;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemCountText;
    [SerializeField] private Image itemIcon;

    private void Awake()
    {
        itemInfoCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        parentAfterDrag = transform.parent;
    }

    public void SetUI(Item itemProfile,int itemCount)
    {
        itemIcon.sprite = itemProfile.itemIcon;
        itemNameText.text = itemProfile.itemName;
        itemTypeText.text = itemProfile.itemType.ToString();
        itemCountText.text = itemCount.ToString();
        
        itemIcon.gameObject.SetActive(true);
    }

    public void ClearUI()
    {
        itemIcon.sprite = null;
        itemNameText.text = String.Empty;
        itemTypeText.text = String.Empty;
        itemCountText.text = String.Empty;
        
        itemIcon.gameObject.SetActive(false);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        currentSlot.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        currentSlot.raycastTarget = true;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        /*if (parentAfterDrag == null)
        {
            return;
        }*/
        if (parentAfterDrag.GetComponent<DropSlot>().currentSlot.GetComponent<ItemSlotUI>().ItemProfile == null)
        {
            return;
        }
        itemInfoCanvas.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoCanvas.SetActive(false);
    }
}

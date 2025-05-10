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
    public GameObject buttonSlot;
    public Transform parentAfterDrag;
    
    [Space(10)]  
    [Header("Item Slot UI")] 
    [SerializeField] private GameObject itemInfoCanvas;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemCountText;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject equipRing;

    private void Awake()
    {
        itemInfoCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        parentAfterDrag = transform.parent;
    }
    
    public void EquipItem()
    {
        DropSlot dropSlot = parentAfterDrag.GetComponent<DropSlot>();
        InventoryManager.Instance.UseItem(dropSlot.currentSlot.slotIndex);
    }

    public void SetUI(Item itemProfile,int itemCount,bool isEquipped)
    {
        itemIcon.sprite = itemProfile.itemIcon;
        itemNameText.text = itemProfile.itemName;
        itemTypeText.text = itemProfile.itemType.ToString();
        itemCountText.text = itemCount.ToString();
        equipRing.gameObject.SetActive(isEquipped);

        itemIcon.gameObject.SetActive(true);
    }

    public void ClearUI()
    {
        itemIcon.sprite = null;
        itemNameText.text = String.Empty;
        itemTypeText.text = String.Empty;
        itemCountText.text = String.Empty;
        equipRing.gameObject.SetActive(false);
        
        itemIcon.gameObject.SetActive(false);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        currentSlot.raycastTarget = false;
        buttonSlot.SetActive(false);
        equipRing.GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        currentSlot.raycastTarget = true;
        SetButtonSlot(parentAfterDrag.GetComponent<DropSlot>().currentSlot.ItemProfile.itemType);
        equipRing.GetComponent<Image>().raycastTarget = true;
    }

    public void RemoveItem()
    {
        InventoryManager.Instance.RemoveItem(parentAfterDrag.GetComponent<DropSlot>().currentSlot.ItemProfile);
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
        SetButtonSlot(parentAfterDrag.GetComponent<DropSlot>().currentSlot.ItemProfile.itemType);
        itemInfoCanvas.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoCanvas.SetActive(false);
    }

    private void SetButtonSlot(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Tool:
                buttonSlot.SetActive(true);
                buttonText.text = "EQUIP";
                break;
            case ItemType.CraftedObject:
                buttonSlot.SetActive(true);
                buttonText.text = "USE";
                break;
            default:
                buttonSlot.SetActive(false);
                break;
        }
    }
}

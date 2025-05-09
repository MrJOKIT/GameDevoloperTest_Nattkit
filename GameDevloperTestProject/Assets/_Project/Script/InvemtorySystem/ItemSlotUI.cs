using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Item itemProfile;
    public Item ItemProfile { get { return itemProfile; } private set { itemProfile = value; } }

    [Space(10)] 
    [Header("Item Slot UI")] 
    [SerializeField] private GameObject itemInfoCanvas;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private Image itemIcon;

    private void Awake()
    {
        itemInfoCanvas.SetActive(false);
    }

    public void SetItem(Item item)
    {
        itemProfile = item;
        itemIcon.sprite = itemProfile.itemIcon;
        itemNameText.text = itemProfile.itemName;
        itemTypeText.text = itemProfile.itemType.ToString();
        
        itemIcon.gameObject.SetActive(true);
    }

    public void ClearItem()
    {
        itemProfile = null;
        itemIcon.sprite = null;
        itemNameText.text = String.Empty;
        itemTypeText.text = String.Empty;
        
        itemIcon.gameObject.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemProfile == null)
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

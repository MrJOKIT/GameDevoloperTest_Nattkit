using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : InteractObject
{
    [SerializeField] private Item itemProfile;
    [SerializeField] private bool infiniteItem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onPlayer)
        {
            if (InventoryManager.Instance.CheckInventoryFull(itemProfile))
            {
                Announcement.Instance.SetAnnouncementText("Inventory is full");
                Debug.Log("Inventory is full");
                return;
            }
            InventoryManager.Instance.AddItem(itemProfile);
            if (infiniteItem)
            {
                return;
            }
            Destroy(gameObject);
        }
    }
}

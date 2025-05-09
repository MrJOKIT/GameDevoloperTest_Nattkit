using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : InteractObject
{
    [SerializeField] private Item itemProfile;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onPlayer)
        {
            if (InventoryManager.Instance.CheckInventoryFull())
            {
                Announcement.Instance.SetAnnouncementText("Inventory is full");
                Debug.Log("Inventory is full");
                return;
            }
            InventoryManager.Instance.AddItem(itemProfile);
            Destroy(gameObject);
        }
    }
}

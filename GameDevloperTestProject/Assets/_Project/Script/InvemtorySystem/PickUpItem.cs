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
            InventoryManager.Instance.AddItem(itemProfile);
            Destroy(gameObject);
        }
    }
}

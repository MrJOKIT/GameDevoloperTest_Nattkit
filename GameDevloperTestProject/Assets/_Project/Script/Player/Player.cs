using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Interact")]
    [SerializeField] private bool canInteract = false;
    public bool CanInteract { get { return canInteract; } set { canInteract = value; } }
    [SerializeField] private GameObject interactingUI;

    private void Update()
    {
        interactingUI.SetActive(canInteract);
    }
}

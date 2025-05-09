using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stat")] 
    [SerializeField] private int playerMaxHealth = 5;
    [SerializeField] private int playerHealth = 5;
    [SerializeField] private int playerDamage = 1;
    [Header("Interact")]
    [SerializeField] private bool canInteract = false;
    public bool CanInteract { get { return canInteract; } set { canInteract = value; } }
    [SerializeField] private GameObject interactingUI;

    private void Update()
    {
        interactingUI.SetActive(canInteract);
    }
}

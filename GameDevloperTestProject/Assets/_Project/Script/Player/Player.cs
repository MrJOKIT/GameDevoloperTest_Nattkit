using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player Stat")] 
    [SerializeField] private float playerMaxHealth = 100;
    [SerializeField] private float playerHealth = 100;
    [Space(10)]
    [SerializeField] private float playerMaxMana = 100;
    [SerializeField] private float playerMana = 100;
    [Space(10)]
    [SerializeField] private int playerDamage = 1;
    [Header("Interact")]
    [SerializeField] private bool canInteract = false;
    public bool CanInteract { get { return canInteract; } set { canInteract = value; } }
    [SerializeField] private GameObject interactingUI;
    [Header("Player UI")] 
    [SerializeField] private Image playerHpBar;

    private void Update()
    {
        interactingUI.SetActive(canInteract);
    }

    private void SetupPlayer()
    {
        playerHealth = playerMaxHealth;
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        playerHpBar.fillAmount = playerHealth / playerMaxHealth;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            PlayerDead();
        }
    }

    public void Heal(float heal)
    {
        playerHealth += heal;
        playerHpBar.fillAmount = playerHealth / playerMaxHealth;
        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
            Announcement.Instance.SetAnnouncementText("HP IS MAX");
        }
        
    }

    public void ExitCombatArea()
    {
        playerHealth = playerMaxHealth;
    }

    [ContextMenu("Player Dead")]
    private void PlayerDead()
    {
        Announcement.Instance.SetAnnouncementText("Player Dead");
        transform.position = Vector3.zero;
        playerHealth = playerMaxHealth;
    }
    
}

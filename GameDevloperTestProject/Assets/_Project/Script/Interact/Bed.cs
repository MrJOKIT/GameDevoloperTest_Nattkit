using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField] private bool onPlayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onPlayer)
        {
            TimeHopManager.Instance.TriggerTimeHop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayer = false;
        }
    }
}

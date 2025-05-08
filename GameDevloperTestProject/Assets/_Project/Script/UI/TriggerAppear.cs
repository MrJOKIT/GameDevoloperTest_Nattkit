using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAppear : MonoBehaviour
{
    [SerializeField] private GameObject appearObject;

    private void Awake()
    {
        appearObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            appearObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            appearObject.SetActive(false);
        }
    }
}

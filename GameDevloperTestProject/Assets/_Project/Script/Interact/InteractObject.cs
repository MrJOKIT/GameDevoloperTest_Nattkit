using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractObject : MonoBehaviour
{
    protected bool onPlayer;
    
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayer = true;
            other.gameObject.GetComponent<Player>().CanInteract = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayer = false;
            other.gameObject.GetComponent<Player>().CanInteract = false;
        }
    }
}

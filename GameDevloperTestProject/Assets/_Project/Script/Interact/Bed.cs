using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractObject
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onPlayer)
        {
            TimeHopManager.Instance.TriggerTimeHop();
        }
    }
}

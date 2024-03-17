using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchArea : MonoBehaviour
{
    
    //Set goblin dash
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IDashable>(out var target))
        {
            target.canDash = false;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<IDashable>(out var target))
        {
            target.canDash = true;
        }
    }
}

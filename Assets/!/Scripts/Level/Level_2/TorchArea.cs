using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchArea : MonoBehaviour
{
    public List<GameObject> inAreaDashableObject = new List<GameObject>();

    //Set goblin dash
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IDashable>(out var target))
        {
            target.canDash = false;
            target.InLight(true);

            inAreaDashableObject.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<IDashable>(out var target))
        {
            target.canDash = true;
            target.InLight(false);
            inAreaDashableObject.Remove(other.gameObject);
        }
    }

    private void OnDisable()
    {
        foreach (var obj in inAreaDashableObject)
        {
            obj.GetComponent<IDashable>().canDash = true;
            obj.GetComponent<IDashable>().InLight(false);

        }
    }
}
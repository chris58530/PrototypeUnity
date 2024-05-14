using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloblinDestroyZone : MonoBehaviour
{
    private void OnEnable()
    {
        OpenCollider(false);
        AbilityOnFire.onFire += OpenCollider;
    }

    private void OnDisable()
    {
        AbilityOnFire.onFire -= OpenCollider;
    }

    void OpenCollider(bool isOpen)
    {
        if (isOpen)
            GetComponent<Collider>().enabled = true;
        else
            GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.GetComponent<GoblinBase>()) return;
        Destroy(other.gameObject);
    }
}
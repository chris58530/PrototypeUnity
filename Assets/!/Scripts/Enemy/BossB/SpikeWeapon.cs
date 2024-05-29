using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWeapon : MonoBehaviour
{
    private void OnEnable()
    {
        this.gameObject.GetComponent<Collider>().enabled = true;

        StartCoroutine(DisableRoutine());
    }

    IEnumerator DisableRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        this.gameObject.GetComponent<Collider>().enabled = false;
    }
}
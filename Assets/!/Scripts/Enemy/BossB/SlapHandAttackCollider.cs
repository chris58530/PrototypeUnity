using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapHandAttackCollider : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DisableGmaeObject());
    }

    IEnumerator DisableGmaeObject()
    {
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
    }
}
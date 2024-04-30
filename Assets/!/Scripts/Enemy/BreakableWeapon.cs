using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Interface;
using UnityEngine;

public class BreakableWeapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IBreakable>(out IBreakable target))
            target.OnTakeAttack();
    }
}

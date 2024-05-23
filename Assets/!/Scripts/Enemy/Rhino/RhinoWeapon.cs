using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoWeapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<StoneWall>(out var stoneWall))
        {
            stoneWall.TakeRhinoAttack();
        }
    }
}
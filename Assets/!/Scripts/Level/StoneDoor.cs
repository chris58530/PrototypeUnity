using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDoor : MonoBehaviour,IDamageable
{
 
    public void OnTakeDamage(int value)
    {
        OnDied();
    }

    public void OnDied()
    {
        Destroy(gameObject);
    }
}

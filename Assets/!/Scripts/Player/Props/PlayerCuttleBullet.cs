using System.Collections;
using System.Collections.Generic;
using _.Scripts.Interface;
using UnityEngine;

public class PlayerCuttleBullet : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        //OnTrigger enemy will do 
        Debug.Log("Use Strength TriggerEffect ");
        if (other.TryGetComponent<IShieldable>(out IShieldable target))
            target.OnTakeShield(1);

        if (!other.TryGetComponent<IDamageable>(out var obj)) return;
        obj.OnTakeDamage(1000);
    }
}
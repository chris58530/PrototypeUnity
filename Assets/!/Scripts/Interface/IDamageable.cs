using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void OnTakeDamage(int value,Vector3 sparkleDirection ,Quaternion rotation);
    void OnDied();
}
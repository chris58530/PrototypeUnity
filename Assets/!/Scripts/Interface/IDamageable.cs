using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void OnTakeDamage(float value);
    void OnKnock(Transform trans);
    void OnDied();
}
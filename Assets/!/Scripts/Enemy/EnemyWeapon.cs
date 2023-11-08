using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private int attackValue;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<IDamageable>(out var damageObj)) return;
        if (other.gameObject.layer != 6) return;

        damageObj.OnTakeDamage(attackValue);
    }
}

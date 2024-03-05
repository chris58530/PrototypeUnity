using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private int attackValue = 1;
    [SerializeField] private LayerMask mask;


    private void OnTriggerEnter(Collider other)
    {
        if ((mask & (1 << other.gameObject.layer)) == 0) return;

        Damage(other);
        Knock(other);
    }

    void Damage(Collider other)
    {
        if (!other.TryGetComponent<IDamageable>(out var obj)) return;

        obj.OnTakeDamage(attackValue);

        Debug.Log($"{other.name} get {attackValue} damage");
    }

    void Knock(Collider other)
    {
        if (!other.TryGetComponent<IKnockable>(out var obj)) return;
        obj.OnKnock(this.transform);
    }
}
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private int attackValue;
    [SerializeField] private LayerMask mask;
    private Collider _collider => GetComponent<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if ((mask & (1 << other.gameObject.layer)) == 0) return;

        Damage(other);
        Knock(other);
    }

    void Damage(Collider other)
    {
        if (!other.TryGetComponent<IDamageable>(out var obj)) return;
        _collider.enabled = false;
        obj.OnTakeDamage(attackValue);

        Debug.Log($"{other.name} get {attackValue} damage");
    }

    void Knock(Collider other)
    {
        if (!other.TryGetComponent<IKnockable>(out var obj)) return;

        _collider.enabled = false;
        obj.OnKnock(this.transform);
    }
}
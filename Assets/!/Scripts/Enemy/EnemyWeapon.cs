using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private int attackValue;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer ==LayerMask.NameToLayer("UnDamageable")) return;

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
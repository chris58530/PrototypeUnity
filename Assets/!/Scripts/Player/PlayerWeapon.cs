using System;
using UnityEngine;

namespace _.Scripts.Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private int attackValue;


        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IDamageable>(out var damageObj)) return;
            if (other.gameObject.layer != 7) return;

            damageObj.OnTakeDamage(attackValue);
        }
    }
}
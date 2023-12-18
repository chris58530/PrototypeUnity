using System;
using _.Scripts.Event;
using _.Scripts.Tools;
using TMPro;
using UnityEngine;
using UniRx;

namespace _.Scripts.Player
{
    public class NormalWeapon : Weapon
    {
        private void Start()
        {
            transform.parent = GameObject.Find("SwordPoint").transform;
            transform.gameObject.SetActive(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IDamageable>(out var damageObj)) return;
            if (other.gameObject.layer != 7 ) return;

            damageObj.OnTakeDamage(attackValue);
            PlayerActions.onHitEnemy.Invoke(1);
            //dubug
            TMP_Text t = GameObject.Find("AttackValueText").GetComponent<TMP_Text>();
            t.text = (attackValue ).ToString();
        }
    }
}
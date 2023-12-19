using _.Scripts.Event;
using TMPro;
using UnityEngine;

namespace _.Scripts.Player
{
    public class UlitWeapon : Weapon
    {
        private void Start()
        {
            transform.parent = GameObject.Find("SwordPoint").transform;
            transform.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IDamageable>(out var damageObj)) return;
            if (other.gameObject.layer != 7) return;

            damageObj.OnTakeDamage(attackValue);
            //dubug
            TMP_Text t = GameObject.Find("AttackValueText").GetComponent<TMP_Text>();
            t.text = (attackValue).ToString();
        }
    }
}
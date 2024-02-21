using _.Scripts.Event;
using TMPro;
using UnityEngine;

namespace @_.Scripts.Player.Props
{
    public class AttackWeapon : Weapon
    {
        [SerializeField, Header("Put the sword model to here")]
        private GameObject swordTransform;

        private void Start()
        {
            transform.parent = swordTransform.transform;
            transform.gameObject.SetActive(false);

        }

        private void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IDamageable>(out var damageObj)) return;
            //Enemy layer
            if (other.gameObject.layer != 7) return;

            damageObj.OnTakeDamage(attackValue);
            Debug.Log("攻擊了 : " + attackValue);
            attackAction?.Invoke(other);
            PlayerActions.onHitEnemy?.Invoke(0.1f);
            //dubug
            TMP_Text t = GameObject.Find("AttackValueText").GetComponent<TMP_Text>();
            t.text = (attackValue).ToString();
        }
    }
}


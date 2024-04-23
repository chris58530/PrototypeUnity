using _.Scripts.Event;
using TMPro;
using UnityEngine;

namespace @_.Scripts.Player.Props
{
    public class AttackWeapon : Weapon
    {
        [SerializeField, Header("Put the sword model to here")]
        private GameObject swordTransform;

        [SerializeField] private LayerMask mask;

        // [Header("On Hit Effect")] [SerializeField]
        // private ParticleSystem onHitEffect;
        [Header("Gamepad vibrate setting")] [SerializeField]
        private float low;

        [SerializeField] private float high;
        [SerializeField] private float time;
        private void Start()
        {
            transform.parent = swordTransform.transform;
            transform.GetComponent<Collider>().enabled = false;
        }


        private void OnTriggerEnter(Collider other)
        {
           

            if (!other.TryGetComponent<IDamageable>(out var damageObj)) return;
            //Enemy layer
            if ((mask & (1 << other.gameObject.layer)) == 0) return;

            
            //Damage frist then use attackActions effect attack
            damageObj.OnTakeDamage(attackValue);
            //Damage frist then use attackActions effect attack (ability)
            attackAction?.Invoke(other);
            
            
            
            Debug.Log("攻擊了 : " + attackValue);
            PlayerActions.onHitEnemy?.Invoke();
            //dubug
            TMP_Text t = GameObject.Find("AttackValueText").GetComponent<TMP_Text>();
            t.text = (attackValue).ToString();
            
            //手把震動
            SystemActions.onGamePadVibrate?.Invoke(low, high, time);

            // var closestPoint = other.bounds.ClosestPoint(transform.position);
            // Instantiate(onHitEffect, closestPoint, Quaternion.identity);
        }

        public void AddLayerFromMask(bool isAdd, string layerName)
        {
            if (isAdd)
                mask |= LayerMask.GetMask(layerName);
            else
                mask &= ~LayerMask.GetMask(layerName);
        }
    }
}
using _.Scripts.Event;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

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
            Vector3 thisPosition = transform.position;
            Vector3 collisionPoint = other.ClosestPoint(thisPosition);
            
            Quaternion rotation = Quaternion.LookRotation(collisionPoint - thisPosition);
            damageObj.OnTakeDamage(attackValue, collisionPoint, -rotation);
            //Damage frist then use attackActions effect attack (ability)
            attackAction?.Invoke(other);


            Debug.Log("主角攻擊了" + other.name + " 造成了" + attackValue + "傷害");

            PlayerActions.onHitEnemy?.Invoke();

            //計算接觸點 觸發特效
            //手把震動
            SystemActions.onGamePadVibrate?.Invoke(low, high, time);
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
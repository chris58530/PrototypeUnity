using System;
using _.Scripts.Interface;
using TMPro;
using UniRx;
using UnityEngine;

namespace @_.Scripts.Player.Props
{
    public class AbilitySystem : PlayerAttackSystem
    {
        [Header("Insert state exit time")] public float insertTime;
        private AbilityWeapon _abilityWeapon;
        private BoxCollider _abilityWeaponCollider;
        public AbilityWeapon.AbilityType GetCurrentAbility => _abilityWeapon.currentAbility;
        public bool isBlockInsert;

        private void Awake()
        {
            _abilityWeapon = GetComponentInChildren<AbilityWeapon>();
            _abilityWeaponCollider = _abilityWeapon.GetComponent<BoxCollider>();
        }

        private void Start()
        {
            CancelAttack();
        }


        public void Attack()
        {
            Observable.EveryFixedUpdate().First().Delay(TimeSpan.FromSeconds(0.04f)).Subscribe(_ =>
            {
                // transform.LookAt(GetDirection());
                _abilityWeaponCollider.enabled = true;
            }).AddTo(this);

            // if (autoTurnAroundDetect.NearContainers(this.transform) == null) return;
            // transform.LookAt(autoTurnAroundDetect.NearContainers(this.transform));
            
            
            // Vector3 dir = autoTurnAroundDetect.NearContainers(this.transform).transform.position;
            // Quaternion toRotation = Quaternion.LookRotation(dir, transform.up);
            // transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1000 * Time.deltaTime);

            // Vector3 dir = new Vector3(autoTurnAroundDetect.NearContainers(this.transform).position.x, 
            //     0, autoTurnAroundDetect.NearContainers(this.transform).position.y);
            // Quaternion toRotation = Quaternion.LookRotation(dir, transform.up);
            // transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1000 * Time.deltaTime);
        }

        public void CancelAttack()
        {
            _abilityWeaponCollider.enabled = false;
        }
    }
}
using System;
using _.Scripts.Interface;
using TMPro;
using UnityEngine;

namespace @_.Scripts.Player.Props
{
    public class AbilitySystem : PlayerAttackSystem
    {
        [Header("Insert state exit time")] public float insertTime;
        private AbilityWeapon _abilityWeapon;
        private BoxCollider _abilityWeaponCollider;
        private void Awake()
        {
            _abilityWeapon = GetComponentInChildren<AbilityWeapon>();
            _abilityWeaponCollider = _abilityWeapon.GetComponent<BoxCollider>();
        }

        private void Start()
        {
            CancelAttack();
        }

        private void Update()
        {
            _abilityWeapon.ExecuteAblilty();
        }

        public void Attack()
        {
            transform.LookAt(GetDirection());
            _abilityWeaponCollider.enabled = true;
            AbilityWeaponAnimator.Instance?.PlayerAnimation(AbilityWeaponAnimator.AnimationName.Azbsword);
        }

        public void CancelAttack()
        {
            _abilityWeaponCollider.enabled = false;
        }
    }
}
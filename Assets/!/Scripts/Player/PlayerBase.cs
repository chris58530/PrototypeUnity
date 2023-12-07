using System;
using _.Scripts.Event;
using _.Scripts.Tools;
using UniRx;
using UnityEngine;

namespace _.Scripts.Player
{
    public class PlayerBase : MonoBehaviour, IDamageable
    {
        public bool hasSword;
        public float maxHpValue;
        [HideInInspector] public ReactiveProperty<float> currentHpValue = new ReactiveProperty<float>();

        public float maxSkillValue;
        [HideInInspector] public ReactiveProperty<float> currentSkillValue = new ReactiveProperty<float>();
        
        public float maxUltimateValue;
        [HideInInspector] public ReactiveProperty<float> currentUltimateValue = new ReactiveProperty<float>();
        
        
        public bool getHurt;

        private void Start()
        {
            Initialize();
            currentHpValue.Skip(1).Subscribe(_ =>
            {
                getHurt = true;
            }).AddTo(this);
        }

        void Initialize()
        {
            currentHpValue.Value = maxHpValue;
            currentSkillValue.Value = 0;
            currentUltimateValue.Value = 0;
        }


        public void SetSkillValue(float value)
        {
            currentSkillValue.Value += value;
            if (currentSkillValue.Value >= maxSkillValue)
                currentSkillValue.Value = maxSkillValue;
        }

        public void ResetSkillValue()
        {
            currentSkillValue.Value = 0;
        }
        public void SetUltimateValue(float value)
        {
            currentSkillValue.Value += value;
            if (currentUltimateValue.Value >= maxUltimateValue)
                currentUltimateValue.Value = maxUltimateValue;
        }

        public void OnTakeDamage(float value)
        {
            currentHpValue.Value -= value;
            if (currentHpValue.Value >= maxHpValue)
                currentHpValue.Value = maxHpValue;
        }
        public void OnDied()
        {
        }

        private void OnEnable()
        {
            PlayerActions.onHitEnemy += SetSkillValue;
            PlayerActions.onHitPlayer += ResetSkillValue;
        }


        private void OnDisable()
        {
            PlayerActions.onHitEnemy -= SetSkillValue;
            PlayerActions.onHitPlayer -= ResetSkillValue;
        }
    }
}
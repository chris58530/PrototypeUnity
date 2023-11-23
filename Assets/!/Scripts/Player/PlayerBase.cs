using System;
using UniRx;
using UnityEngine;

namespace _.Scripts.Player
{
    public class PlayerBase : MonoBehaviour,IDamageable
    {
        public float maxHpValue;
        [HideInInspector] public ReactiveProperty<float> currentHpValue = new ReactiveProperty<float>();

        public float maxSkillValue;
        [HideInInspector] public ReactiveProperty<float> currentSkillValue = new ReactiveProperty<float>();

        private void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            currentHpValue.Value = maxHpValue;
            currentSkillValue.Value = 0;
        }

   

        public void SetSkillValue(float value)
        {
            currentSkillValue.Value += value;
            if (currentSkillValue.Value >= maxSkillValue)
                currentSkillValue.Value = maxSkillValue;
        }

        public void OnTakeDamage(float value)
        {
            currentHpValue.Value -= value;
            if (currentHpValue.Value >= maxHpValue)
                currentHpValue.Value = maxHpValue;
        }

        public void OnDied()
        {
            throw new NotImplementedException();
        }
    }
}
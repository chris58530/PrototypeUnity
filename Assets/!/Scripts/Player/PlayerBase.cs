using System;
using _.Scripts.Event;
using _.Scripts.Tools;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace _.Scripts.Player
{
    public class PlayerBase : MonoBehaviour, IDamageable
    {
        public float maxHpValue;
        [HideInInspector] public ReactiveProperty<float> currentHpValue = new ReactiveProperty<float>();
        
        public int maxSwordLevelValue;
        [HideInInspector] public ReactiveProperty<int> currentSwordLevelValue = new ReactiveProperty<int>();

        public float maxUltimateValue;
        [HideInInspector] public ReactiveProperty<float> currentUltimateValue = new ReactiveProperty<float>();
        public float maxShieldValue;
        [HideInInspector] public ReactiveProperty<float> currentShieldValue = new ReactiveProperty<float>();

        public bool getHurt;
        [SerializeField] private float hurtCD;
        private IDisposable _hurtCd;
        [SerializeField] private bool _canHurt = true;

        private void Start()
        {
            Initialize();
            currentHpValue.Skip(1).Subscribe(_ => { getHurt = true; }).AddTo(this);
        }

        void Initialize()
        {
            currentHpValue.Value = maxHpValue;
            currentSwordLevelValue.Value = 0;
            currentUltimateValue.Value = 0;
            currentShieldValue.Value = maxShieldValue;
        }


        public void SetSkillValue(int value)
        {
            currentSwordLevelValue.Value += value;
            if (currentSwordLevelValue.Value >= maxSwordLevelValue)
                currentSwordLevelValue.Value = maxSwordLevelValue;
        }

        public void ResetSkillValue()
        {
            currentSwordLevelValue.Value = 0;
        }

    

        public void OnTakeDamage(float value)
        {
            if (transform.CompareTag("Undamaged")) return;
            if (!_canHurt) return;
            _canHurt = false;
            _hurtCd = Observable.EveryUpdate()
                .Delay(TimeSpan.FromSeconds(hurtCD)).Subscribe(_ => { _canHurt = true; });
            if (currentShieldValue.Value > 0)
            {
                currentShieldValue.Value -= 1;
            }
            else
            {
                currentHpValue.Value -= value;
                if (currentHpValue.Value >= maxHpValue)
                    currentHpValue.Value = maxHpValue;
            }
        }

        public void OnKnock(Transform trans)
        {
            Vector3 offset = (transform.position - trans.position).normalized;
        }

        public void OnDied()
        {
        }

        private void OnEnable()
        {
        }


        private void OnDisable()
        {
        }
    }
}
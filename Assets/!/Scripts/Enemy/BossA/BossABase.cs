using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Interface;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.Serialization;

namespace _.Scripts.Enemy.BossA
{
    [RequireComponent(typeof(BossAController))]
    public class BossABase : Enemy, IDamageable, IShieldable
    {
        public Image hpImage;
        public Image hardHpImage;

        [SerializeField] private float maxHp;
        private ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();
        public bool isShielded;

        [Header("Shield Setting")] //.
        [SerializeField]
        private int shieldValue;

        private void Start()
        {
            Initialize();


            IsShield(true);

            _currentHp.Subscribe(_ =>
            {
                hpImage.fillAmount = _currentHp.Value / maxHp;
                hardHpImage.fillAmount = _currentHp.Value / maxHp;
            }).AddTo(this);
        }

        private void Update()
        {
            if (shieldValue <= 0) bt.SendEvent("Remove_Shield_Event");
        }

        void Initialize()
        {
            _currentHp.Value = maxHp;
        }


        public void OnTakeDamage(int value)
        {
            if (isShielded)
            {
                _currentHp.Value -= value / 5;
            }

            else
                _currentHp.Value -= value;

            if (_currentHp.Value <= 0)
            {
                Debug.Log("Boss A Die");
                OnDied();
            }
        }

        public void OnTakeShield(int removeValue)
        {
            shieldValue -= removeValue;
            Debug.Log("shieldValue - 1");

          
        }

        public void ResetShield()
        {
            shieldValue = 3;
        }

        public void OnDied()
        {
            bt.enabled = false;
            gameObject.GetComponentInChildren<Animator>().Play("Die");
        }

        public void IsShield(bool b)
        {
            if (b)
            {
                hardHpImage.enabled = true;
                hpImage.enabled = false;
            }

            else
            {
                hardHpImage.enabled = false;
                hpImage.enabled = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("RemoveShield"))
            {
            }
        }
    }
}
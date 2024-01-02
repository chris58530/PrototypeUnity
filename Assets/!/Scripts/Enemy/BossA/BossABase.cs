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
    public class BossABase : Enemy, IDamageable
    {
        public Image hpImage;
        public Image hardHpImage;

        [SerializeField] private float maxHp;
        private ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();
        public bool isShielded;

        private void Start()
        {
            Initialize();


            IsShield(true);

            _currentHp.Subscribe(_ => { hpImage.fillAmount = _currentHp.Value / maxHp; hardHpImage.fillAmount = _currentHp.Value / maxHp; }).AddTo(this);
        }

        void Initialize()
        {
            _currentHp.Value = maxHp;
        }


        public void OnTakeDamage(float value)
        {
            if (isShielded)
            {
                _currentHp.Value -= value / 5;
            }

            else
                _currentHp.Value -= value;

            if (_currentHp.Value <= 0)
            {
                Debug.Log("死亡");
                OnDied();
            }
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
                bt.SendEvent("BombHurt");
            }
        }
    }
}
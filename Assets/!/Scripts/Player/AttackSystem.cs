using System;
using _.Scripts.Event;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace _.Scripts.Player
{
    public class AttackSystem : PlayerAttackSystem
    {
        [Header("Attack Setting")] //
        [SerializeField]
        public float[] attackTime;

        [SerializeField] public int attackCount = 0;
        [SerializeField] private float chanceTime;
        [SerializeField] public bool finishAttack;
        [SerializeField] private GameObject weaponCollider;
        [SerializeField] private GameObject attackChancePreview;


        [Header("Fail Setting")] //
        [SerializeField]
        public float failTime;

        public bool finsihFail;
        private IDisposable _failTimer;


        public void Fail()
        {
            finsihFail = false;
            _failTimer = Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(failTime)).Subscribe(_ =>
            {
                finsihFail = true;
            }).AddTo(this);
        }


        public void Attack()
        {
            chanceTimer?.Dispose();


            //audio
            if (attackCount == 0)
                AudioManager.Instance.PlaySFX("Attack1");
            else if (attackCount == 1)
                AudioManager.Instance.PlaySFX("Attack2");
            else if (attackCount == 2)
                AudioManager.Instance.PlaySFX("Attack3");

            //接技 保持攻擊不中斷 Q1可以接走路再接Q2
            if (attackCount < 2)
                attackCount++;
            else attackCount = 0;

            transform.LookAt(GetDirection());

            chanceTimer = Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(chanceTime))
                .First().Subscribe(_ => { finishAttack = true; });

            weaponCollider.SetActive(true);
        }

        public float AttackTime(int count)
        {
            float time = 0;
            time = attackTime[count];
            return time;
        }

        public void CancelAttack()
        {
            weaponCollider.SetActive(false);
        }


        public void AttackChancePreview(Color color)
        {
            attackChancePreview.GetComponent<MeshRenderer>().material.color = color;
        }
    }
}
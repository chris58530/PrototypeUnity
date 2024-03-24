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
            UseNormalAttack();
        }

        private void UseNormalAttack()
        {
            chanceTimer?.Dispose();

            //sword effect
            PlayerActions.onPlayerAttackEffect.Invoke(attackCount, 1);
            //audio
            if (attackCount == 0)
            {
                AudioManager.Instance.PlaySFX("Attack1");
                // if (isHeammer)
                //     AbilityWeaponAnimator.Instance.PlayerAnimation(AbilityWeaponAnimator.AnimationName.Q1);
            }
            else if (attackCount == 1)
            {
                AudioManager.Instance.PlaySFX("Attack2");
                // if (isHeammer)
                //     AbilityWeaponAnimator.Instance.PlayerAnimation(AbilityWeaponAnimator.AnimationName.Q2);
            }
            else if (attackCount == 2)
            {
                AudioManager.Instance.PlaySFX("Attack3");
                // if (isHeammer)
                //     AbilityWeaponAnimator.Instance.PlayerAnimation(AbilityWeaponAnimator.AnimationName.Q3);
            }

            //接技 保持攻擊不中斷 Q1可以接走路再接Q2
            if (attackCount < 2)
                attackCount++;
            else attackCount = 0;


            chanceTimer = Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(chanceTime))
                .First().Subscribe(_ => { finishAttack = true; });

            weaponCollider.GetComponent<Collider>().enabled = true;
        }

        public void FaceMouseInputPosition()
        {
            transform.LookAt(GetDirection());

        }

        public float AttackTime(int count)
        {
            float time = 0;
            time = attackTime[count];
            return time;
        }

        public void CancelAttack()
        {
            weaponCollider.GetComponent<Collider>().enabled = false;
        }


        public void AttackChancePreview(Color color)
        {
            attackChancePreview.GetComponent<MeshRenderer>().material.color = color;
        }
        
    }
}
using System;
using _.Scripts.Event;
using UniRx;
using UnityEngine;

namespace @_.Scripts.Player.Props
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
        [SerializeField] private GameObject weaponColliderQ3;


        public void Reset()
        {
            chanceTimer?.Dispose();
            finishAttack = false;
            attackCount = 0;
        }


        public void Attack()
        {
            UseNormalAttack();
        }

        public void UseQ3Attack()
        {
            //與UseNormalAttack()的差異在開啟的攻擊Collider不一樣
            chanceTimer?.Dispose();

            //sword effect
            PlayerActions.onPlayerAttackEffect.Invoke(attackCount, 1);
            //audio
            PlayAudio(attackCount);


            //接技 保持攻擊不中斷 Q1可以接走路再接Q2
            if (attackCount < 2)
                attackCount++;
            else attackCount = 0;


            chanceTimer = Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(chanceTime + 0.2f))
                .First().Subscribe(_ => { finishAttack = true; });

            weaponColliderQ3.GetComponent<Collider>().enabled = true;
         
        }

        private void UseNormalAttack()
        {
            chanceTimer?.Dispose();

            //sword effect
            PlayerActions.onPlayerAttackEffect.Invoke(attackCount, 1);
            PlayAudio(attackCount);
            //audio


            //接技 保持攻擊不中斷 Q1可以接走路再接Q2
            if (attackCount < 2)
                attackCount++;
            else attackCount = 0;


            chanceTimer = Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(chanceTime))
                .First().Subscribe(_ => { finishAttack = true; });

            weaponCollider.GetComponent<Collider>().enabled = true;
        }

        public void UseAbilityAttack(AbilityWeapon.AbilityType abilityType)
        {
            chanceTimer?.Dispose();
            weaponCollider.GetComponent<Collider>().enabled = true;

            switch (abilityType)
            {
                case AbilityWeapon.AbilityType.Key:
                    //Q3的音樂和特效
                    PlayAudio(2);
                    PlayerActions.onPlayerAttackEffect.Invoke(2, 1);

                    break;
                case AbilityWeapon.AbilityType.BreakWall:
                    PlayAudio(2);

                    break;
                case AbilityWeapon.AbilityType.Gun:
                    break;
                case AbilityWeapon.AbilityType.None:
                    break;
                default:
                    Debug.LogWarning("Unknown ability type!");
                    break;
            }
        }

        public void AutoDetect()
        {
            //自動校正
            transform.LookAt(autoTurnAroundDetect.NearEnemy(transform));
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
            weaponColliderQ3.GetComponent<Collider>().enabled = false;
        }

        public void PlayAudio(int count)
        {
            if (count == 0)
            {
                AudioManager.Instance.PlaySFX("Attack1");
            }
            else if (count == 1)
            {
                AudioManager.Instance.PlaySFX("Attack2");
            }
            else if (count == 2)
            {
                AudioManager.Instance.PlaySFX("Attack3");
            }
        }
    }
}
using System;
using _.Scripts.Event;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace _.Scripts.Player
{
    public class AttackSystem : PlayerAttackSystem
    {
        [Header("No Sword Setting")] [SerializeField]
        private GameObject swordPoint;

        public bool hasSword;
        [SerializeField] private GameObject swordParent;

        [Header("Attack Setting")] //
        [SerializeField]
        public float[] attackTime;

        [SerializeField] public int attackCount = 0;
        [SerializeField] private float chanceTime;
        [SerializeField] public bool finishAttack;
        [SerializeField] private GameObject weaponCollider;
        [SerializeField] private GameObject attackChancePreview;


        [Header("Sword Setting")] //
        [SerializeField]
        public float chargeTime;

        [SerializeField] private float swordResetTime;

        [Header("Fail Setting")] //
        [SerializeField]
        public float failTime;

        public bool finsihFail;
        private IDisposable _failTimer;


        [SerializeField] private float[] swordScaleValue;

        private void Start()
        {
            SetSwordLevel(1);
        }

        public void Fail()
        {
            finsihFail = false;
            _failTimer = Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(failTime)).Subscribe(_ =>
            {
                finsihFail = true;
            }).AddTo(this);
        }

        public void NoSword()
        {
            swordPoint.transform.parent = null;

            hasSword = false;
            weaponCollider.SetActive(true);

            attackWeapon.Charge(chargeTime, playerBase.currentShieldValue.Value);
        }

        public void Attack()
        {
            chanceTimer?.Dispose();

            //sword effect
            PlayerActions.onPlayerAttackEffect.Invoke(attackCount, scale);
            //audio
            if (attackCount == 0)
                AudioManager.Instance.PlaySFX("Attack1");
            if (attackCount == 1)
                AudioManager.Instance.PlaySFX("Attack2");
            if (attackCount == 2)
            {
                Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(0.2f)).Subscribe(_ =>
                {
                    AudioManager.Instance.PlaySFX("Attack3");
                }).AddTo(this);
            }

            //接技 保持攻擊不中斷 Q1可以接走路再接Q2
            if (attackCount < 2)
                attackCount++;
            else attackCount = 0;

            transform.LookAt(GetDirection());

            chanceTimer = Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(chanceTime))
                .First().Subscribe(_ => { finishAttack = true; });
            //一段時間沒打就損失魔力條
            // lastAttack = Observable.EveryUpdate().First()
            //     .Subscribe(_ => { InvokeRepeating(nameof(DecreaseSkill), decreaseSkillTime, decreaseSkillSpeed); });

            weaponCollider.SetActive(true);
            weaponCollider.transform.localScale = swordPoint.transform.localScale;
        }

        public float AttackTime(int count)
        {
            float time = 0;
            time = attackTime[count];
            return time;
        }

        private int swordLevel;
        private float scale;

        public void IncreaseSwordLevel()
        {
            swordLevelTimer?.Dispose();
            if (playerBase.currentSwordLevelValue.Value >= swordScaleValue.Length) return;

            swordLevel = playerBase.currentSwordLevelValue.Value++;
            if (swordLevel >= swordScaleValue.Length) return;
            scale = swordScaleValue[swordLevel];
            swordPoint.transform.localScale = new Vector3(scale, scale, scale);

            swordLevelTimer = Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(swordResetTime)).First()
                .Subscribe(_ => { SetSwordLevel(0); }).AddTo(this);
        }

        public void SetSwordLevel(int count)
        {
            playerBase.currentSwordLevelValue.Value = count;
            scale = swordScaleValue[count];
            swordPoint.transform.localScale = new Vector3(scale, scale, scale);
        }

        public void CancelAttack()
        {
            weaponCollider.SetActive(false);
        }


        public void AttackChancePreview(Color color)
        {
            attackChancePreview.GetComponent<MeshRenderer>().material.color = color;
        }

        private void OnTriggerStay(Collider other)
        {
            //test
            if (other.gameObject.CompareTag("Sword") && !hasSword && Input.GetKey(KeyCode.E))
            {
                swordPoint.transform.parent = swordParent.transform;
                swordPoint.transform.position = swordParent.transform.position;
                swordPoint.transform.rotation = swordParent.transform.rotation;
                hasSword = true;
                weaponCollider.SetActive(false);
                playerBase.currentShieldValue.Value = attackWeapon.PickUpAndGetValue();
            }
        }
    }
}
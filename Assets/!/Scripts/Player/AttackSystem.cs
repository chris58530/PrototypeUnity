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
        public float attackTime;

        [SerializeField] public int attackCount = 0;
        [SerializeField] private float chanceTime;
        [SerializeField] public bool finishAttack;
        [SerializeField] private GameObject weaponCollider;
        [SerializeField] private GameObject attackChancePreview;
        [SerializeField] private float decreaseSkillTime;
        [SerializeField] private float decreaseSkillSpeed;

        [Header("Sword Setting")] //
        [SerializeField]
        public float chargeTime;

        [SerializeField] private float[] swordScaleValue;


        public void NoSword()
        {
            swordPoint.transform.parent = null;

            hasSword = false;
            weaponCollider.SetActive(true);

            playerSword.Charge(chargeTime, playerBase.currentShieldValue.Value);
        }

        public void Attack(float t)
        {
            lastAttack?.Dispose();
            chanceTimer?.Dispose();

            if (IsInvoking(nameof(DecreaseSkill)))
                CancelInvoke(nameof(DecreaseSkill));

            //接技 保持攻擊不中斷 Q1可以接走路再接Q2
            if (attackCount < 2)
                attackCount++;
            else attackCount = 0;

            transform.LookAt(GetDirection());

            chanceTimer = Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(chanceTime))
                .First().Subscribe(_ =>
                {
                    finishAttack = true;
                    attackCount = 0;
                });
            //一段時間沒打就損失魔力條
            // lastAttack = Observable.EveryUpdate().First()
            //     .Subscribe(_ => { InvokeRepeating(nameof(DecreaseSkill), decreaseSkillTime, decreaseSkillSpeed); });

            weaponCollider.SetActive(true);
            weaponCollider.transform.localScale = swordPoint.transform.localScale;
        }

        public void IncreaseSwordLevel()
        {
            if(playerBase.currentSwordLevelValue.Value>=swordScaleValue.Length)return;
            
            int swordLevel = playerBase.currentSwordLevelValue.Value++;
            if (swordLevel >= swordScaleValue.Length) return;
            Debug.Log(swordLevel);
            float scale = swordScaleValue[swordLevel];
            swordPoint.transform.localScale = new Vector3(scale, scale, scale);
        }

        public void DiscreaseSwordLevel(int count)
        {

            playerBase.currentSwordLevelValue.Value = count;
            float scale = swordScaleValue[count];
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
                playerBase.currentShieldValue.Value = playerSword.PickUpAndGetValue();
            }
        }
    }
}
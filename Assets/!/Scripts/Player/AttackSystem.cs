using System;
using _.Scripts.Event;
using UniRx;
using UnityEngine;

namespace _.Scripts.Player
{
    public class AttackSystem : PlayerAttackSystem
    {
        [Header("No Sword Setting")] [SerializeField]
        private GameObject swordModle;

        public bool hasSword;
        [SerializeField] private GameObject swordParent;

        [Header("Attack Setting")] //
        [SerializeField]
        public float attackTime;

        [SerializeField] public int attackCount = 0;
        [SerializeField] private float chanceTime;
        [SerializeField] public bool finishAttack;
        [SerializeField] private GameObject weapon;
        [SerializeField] private GameObject attackChancePreview;
        [SerializeField] private float decreaseSkillTime;
        [SerializeField] private float decreaseSkillSpeed;

        [Header("Sword Setting")] //
        [SerializeField]
        public float chargeTime;

        private void Start()
        {
            weapon.SetActive(false);
        }

        public void NoSword()
        {
            swordModle.transform.parent = null;
      
            hasSword = false;
            weapon.SetActive(true);

            playerSword.Charge(chargeTime, playerBase.currentShieldValue.Value);
        }

        public void Attack(float t)
        {
            lastAttack?.Dispose();
            chanceDisposable?.Dispose();

            if (IsInvoking(nameof(DecreaseSkill)))
                CancelInvoke(nameof(DecreaseSkill));
            
            //接技
            if (attackCount < 2)
                attackCount++;
            else attackCount = 0;

            transform.LookAt(GetDirection());
            
            chanceDisposable = Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(chanceTime))
                .First().Subscribe(_ =>
                {
                    finishAttack = true;            
                    attackCount = 0;

                });
            
            lastAttack = Observable.EveryUpdate().First()
                .Subscribe(_ => { InvokeRepeating(nameof(DecreaseSkill), decreaseSkillTime, decreaseSkillSpeed); });
            
            weapon.SetActive(true);
        }

        public void CancelAttack()
        {
            weapon.SetActive(false);            

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
                swordModle.transform.parent = swordParent.transform;
                swordModle.transform.position = swordParent.transform.position;
                swordModle.transform.rotation = swordParent.transform.rotation;
                hasSword = true;
                weapon.SetActive(false);
                playerBase.currentShieldValue.Value = playerSword.PickUpAndGetValue();
            }
        }
    }
}
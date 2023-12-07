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

        [SerializeField] public float chanceTime;
        [SerializeField] private GameObject weapon;
        [SerializeField] private GameObject attackChancePreview;
        [SerializeField] private float decreaseSkillTime;
        [SerializeField] private float decreaseSkillSpeed;



        public void NoSword()
        {
            swordModle.transform.parent = null;
            swordModle.transform.rotation = new Quaternion(90, 0, 0, 0);
            swordModle.transform.position += new Vector3(0, 5, 0);
            hasSword = false;
        }
        public void Attack(float t)
        {
            lastAttack?.Dispose();
            chanceDisposable?.Dispose();

            if (IsInvoking(nameof(DecreaseSkill)))
                CancelInvoke(nameof(DecreaseSkill));


            transform.LookAt(GetDirection());
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Sword") && !hasSword)
            {
                swordModle.transform.parent = swordParent.transform;
                swordModle.transform.position = swordParent.transform.position;
                swordModle.transform.rotation = swordParent.transform.rotation;
                hasSword = true;
            }
        }
    }
}
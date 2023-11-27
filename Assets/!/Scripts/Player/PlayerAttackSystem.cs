using System;
using _.Scripts.Event;
using UniRx;
using UnityEngine;

namespace _.Scripts.Player
{
    public class PlayerAttackSystem : MonoBehaviour
    {
        [Header("No Sword Setting")] [SerializeField]
        private GameObject swordModle;

        public bool hasSword;
        [SerializeField] private GameObject swordParent;

        [Header("Attack Setting")] //
        [SerializeField]
        public float attackTime;

        [SerializeField] public float chanceTime;
        [SerializeField] public float failTime;
        [SerializeField] private GameObject weapon;
        [SerializeField] private GameObject attackChancePreview;
        [SerializeField] private float decreaseSkillTime;
        [SerializeField] private float decreaseSkillSpeed;
        public bool CanDoUltimate => _playerBase.currentSkillValue.Value >= 3;

        private PlayerBase _playerBase;
        public bool finishChance;
        public bool isWeak;
        public float weakTime;

        private IDisposable _chanceDis;
        private IDisposable _lastAttack;

        private void Awake()
        {
            _playerBase = GetComponent<PlayerBase>();
        }

        public void NoSwordRoll()
        {
            swordModle.transform.parent = null;

            hasSword = false;
        }

        private IDisposable _weakObser;

        public void SetWeakTime()
        {
            _weakObser = Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(weakTime)).Subscribe(_ =>
            {
                finishChance = false;
            });
        }

        public void ResetWeak()
        {
            _weakObser?.Dispose();
            isWeak = false;
        }

        public void ResetChance()
        {
            _lastAttack?.Dispose();
            _chanceDis?.Dispose();
            finishChance = false;
        }


        public void Attack(float t)
        {
            _lastAttack?.Dispose();
            _chanceDis?.Dispose();
            if (IsInvoking(nameof(DecreaseSkill)))
                CancelInvoke(nameof(DecreaseSkill));

            finishChance = false;
            transform.LookAt(GetDirection());
            _lastAttack = Observable.EveryUpdate().First()
                .Subscribe(_ => { InvokeRepeating(nameof(DecreaseSkill), decreaseSkillTime, decreaseSkillSpeed); });
            weapon.SetActive(true);
        }

        public void DecreaseSkill() //Invoke Mathod
        {
            if (_playerBase.currentSkillValue.Value > 0)
            {
                _playerBase.currentSkillValue.Value -= 1;
            }
        }

        public void ResetSkill()
        {
            _playerBase.currentSkillValue.Value = 0;
        }


        public void CancelAttack()
        {
            finishChance = false;
            _chanceDis = Observable.EveryUpdate()
                .Delay(TimeSpan.FromSeconds(chanceTime))
                .First()
                .Subscribe(_ =>
                {
                    finishChance = true;
                    isWeak = true;
                });
            weapon.SetActive(false);
        }

        public void AttackChancePreview(Color color)
        {
            attackChancePreview.GetComponent<MeshRenderer>().material.color = color;
        }

        public void UseUltimate()
        {
            _playerBase.currentSkillValue.Value = 0;
            _lastAttack?.Dispose();
            _chanceDis?.Dispose();
            if (IsInvoking(nameof(DecreaseSkill)))
                CancelInvoke(nameof(DecreaseSkill));

            finishChance = false;
            transform.LookAt(GetDirection());
            _lastAttack = Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(decreaseSkillTime)).First()
                .Subscribe(_ => { InvokeRepeating(nameof(DecreaseSkill), 0, decreaseSkillSpeed); });
            weapon.SetActive(true);
        }


        private Vector3 GetDirection()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var layerMask = 1 << 9;
            RaycastHit hit;
            var hitpoint = Vector3.zero;
            if (Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                hitpoint = hit.point;
                hitpoint.y = transform.position.y;
                return hitpoint;
            }

            return hitpoint;
        }

        private void OnDisable()
        {
            if (IsInvoking(nameof(DecreaseSkill))) CancelInvoke(nameof(DecreaseSkill));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Sword") && !hasSword)
            {
                swordModle.transform.parent = swordParent.transform;
                swordModle.transform.position = swordParent.transform.position;
                swordModle.transform.rotation = swordParent.transform.rotation;
                finishChance = false;
                hasSword = true;
            }
        }
    }
}
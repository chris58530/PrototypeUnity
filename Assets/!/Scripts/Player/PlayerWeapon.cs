using System;
using _.Scripts.Event;
using TMPro;
using UnityEngine;
using UniRx;

namespace _.Scripts.Player
{
    public enum WeaponType
    {
        Single,
        Multi,
    }

    public class PlayerWeapon : MonoBehaviour
    {
        public  WeaponType weaponType = WeaponType.Single;
        [SerializeField] private GameObject singleWeapon;
        [SerializeField] private GameObject multiWeapon;
        private GameObject _currentAttackWeapon;
        [SerializeField] private int attackValue;
        [SerializeField] private float attackEffectTime = 0.2f;

        private void OnEnable()
        {
            PlayerActions.onPlayerAttack += Attack;
        }

        private void OnDisable()
        {
            PlayerActions.onPlayerAttack -= Attack;
        }

        private void Update()
        {
            Debug.Log("attack");

            if(weaponType == WeaponType.Single)
            {
                TMP_Text t = GameObject.Find("ATKTypeText").GetComponent<TMP_Text>();
                t.text = "Single";
            }else if(weaponType == WeaponType.Multi)
            {
                TMP_Text t = GameObject.Find("ATKTypeText").GetComponent<TMP_Text>();
                t.text = "Multi";
            }
        }

     

        public void Attack() //animation event
        {
            if (weaponType == WeaponType.Single)
                _currentAttackWeapon = singleWeapon;
            else if (weaponType == WeaponType.Multi)
                _currentAttackWeapon = multiWeapon;


            _currentAttackWeapon.SetActive(true);

            Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(attackEffectTime)).Subscribe(_ =>
            {
                _currentAttackWeapon.SetActive(false);
            }).AddTo(this);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IDamageable>(out var damageObj)) return;
            if (other.gameObject.layer != 7) return;

            int combo = FindObjectOfType<PlayerCombo>().combo;
            if (weaponType == WeaponType.Single)
                damageObj.OnTakeDamage(attackValue + combo);
            else if (weaponType == WeaponType.Multi)
                damageObj.OnTakeDamage(attackValue + combo / 2);

            //dubug
            TMP_Text t = GameObject.Find("AttackValueText").GetComponent<TMP_Text>();
            t.text = (attackValue + combo).ToString();
        }
    }
}
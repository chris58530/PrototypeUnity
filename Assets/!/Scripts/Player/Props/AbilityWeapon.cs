using System;
using System.Collections.Generic;
using _.Scripts.Ability;
using _.Scripts.Interface;
using UnityEngine;
using UniRx;

namespace @_.Scripts.Player.Props
{
    public class AbilityWeapon : Weapon
    {
        public enum AbilityType
        {
            None,
            Strength,
            Fire,
            Ice,
            MakeObject,
            Key
        }

        [SerializeField, Header("Put the sword model to here")]
        private GameObject swordTransform;

        [Tooltip("Put all the ability scriptable object to list")] [SerializeField]
        private List<AbilityBase> abilityBase = new List<AbilityBase>();

        private AbilityBase _currentAbilityBase;
        private GameObject _currentInMouthObject;

        [SerializeField] private Transform inMouthObjectTransform;
        private IDisposable _abilityTimer;

        private void Start()
        {
            transform.parent = swordTransform.transform;
            ChangeAbility(AbilityType.None);
        }

        private void Update()
        {
            //for test
            if (Input.GetKeyDown(KeyCode.Z)) ChangeAbility(AbilityType.Strength);
            if (Input.GetKeyDown(KeyCode.X)) ChangeAbility(AbilityType.MakeObject);
            if (Input.GetKeyDown(KeyCode.C)) ChangeAbility(AbilityType.Key);
        }

        public void ExecuteAblilty()
        {
            _currentAbilityBase.AbilityAlgorithm();
        }

        private void ChangeAbility(AbilityType getAbility)
        {
            _abilityTimer?.Dispose();
            
            if (_currentAbilityBase != null)
                _currentAbilityBase.QuitAbilityAlgorithm();
            
            if (_currentInMouthObject != null)
                Destroy(_currentInMouthObject);

            foreach (var ability in abilityBase)
            {
                //搜尋在身上的ability SO 資料 如果有就執行
                if (ability.abilityType == getAbility)
                {
                    _currentAbilityBase = ability;
                    attackValue = _currentAbilityBase.damage;
                    attackAction = _currentAbilityBase.TriggerEffect;
                    _currentAbilityBase.StartAbility();
                    AbilityWeaponAnimator.Instance?.PlayerAnimation(_currentAbilityBase.animationName);

                    //生成 inMouthObject

                    _currentInMouthObject = Instantiate(_currentAbilityBase.inMouthObject,
                        inMouthObjectTransform.position,
                        inMouthObjectTransform.rotation);
                    _currentInMouthObject.transform.parent = inMouthObjectTransform;


                    //計算何時取消能力
                    _abilityTimer = Observable.EveryUpdate().First()
                        .Delay(TimeSpan.FromSeconds(_currentAbilityBase.lifeTime))
                        .Subscribe(_ =>
                        {
                            _currentAbilityBase.QuitAbilityAlgorithm();
                            ChangeAbility(AbilityType.None);
                        }).AddTo(this);

                    Debug.Log(_currentAbilityBase.name);
                    return;
                }
            }
            //搜尋在身上的ability SO 資料 如果沒有就換成 none

            Debug.Log("null current ability");
            ChangeAbility(AbilityType.None);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IAbilityContainer getAbility))
            {
                ChangeAbility(getAbility.GetAbility());
            }
        }

        private void OnDisable()
        {
            if (_currentAbilityBase != null)
                _currentAbilityBase.QuitAbilityAlgorithm();
        }
    }
}
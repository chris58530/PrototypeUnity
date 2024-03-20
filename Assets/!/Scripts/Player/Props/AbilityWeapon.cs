using System;
using System.Collections.Generic;
using _.Scripts.Ability;
using _.Scripts.Enemy;
using _.Scripts.Interface;
using JetBrains.Annotations;
using UnityEngine;
using UniRx;
using UnityEngine.Serialization;

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
            Key,
            Dash,
            BrokeWall,
            Gun,
        }

        [SerializeField, Header("Put the sword model to here")]
        private GameObject swordTransform;

        [FormerlySerializedAs("abilityBase")]
        [Tooltip("Put all the ability scriptable object to list")]
        [SerializeField]
        private List<AbilityBase> abilityBaseList = new List<AbilityBase>();

        private AbilityBase _currentAbilityBase;

        public AbilityType currentAbility = AbilityType.None;

        [SerializeField] private GameObject[] inMouthObjectObjectList;

        private IDisposable _abilityTimer;

        private void Start()
        {
            transform.parent = swordTransform.transform;
            ChangeAbility(AbilityType.None);
        }

        private void Update()
        {
            //for test
            // if (Input.GetKeyDown(KeyCode.Z)) ChangeAbility(AbilityType.None);
            //
            // if (Input.GetKeyDown(KeyCode.X)) ChangeAbility(AbilityType.Strength);
            // if (Input.GetKeyDown(KeyCode.C)) ChangeAbility(AbilityType.BrokeWall);
            //
            // if (Input.GetKeyDown(KeyCode.V)) ChangeAbility(AbilityType.Key);
            // if (Input.GetKeyDown(KeyCode.B)) ChangeAbility(AbilityType.Dash);
            // if (Input.GetKeyDown(KeyCode.N)) ChangeAbility(AbilityType.Gun);
            // if (Input.GetKeyDown(KeyCode.M)) ChangeAbility(AbilityType.Fire);
        }

        public void ExecuteAblilty()
        {
            _currentAbilityBase.AbilityAlgorithm();
        }

        public void ChangeAbility(AbilityType getAbility)
        {
            _abilityTimer?.Dispose();

            if (_currentAbilityBase != null)
                _currentAbilityBase.QuitAbilityAlgorithm();

//make current ability containers gameObject visible 

            foreach (var abilityObject in inMouthObjectObjectList)
            {
                if (abilityObject.GetComponent<IAbilityContainer>().CheckAbility() == getAbility)
                    abilityObject.gameObject.SetActive(true);
                else abilityObject.gameObject.SetActive(false);
            }

//check ability data SO is in abilityBaseList and accept the ability 

            foreach (var ability in abilityBaseList)
            {
                if (ability.abilityType == getAbility)
                {
                    _currentAbilityBase = ability;
                    attackValue = _currentAbilityBase.damage;
                    attackAction = _currentAbilityBase.TriggerEffect;
                    currentAbility = getAbility;
                    _currentAbilityBase.StartAbility();
                    AbilityWeaponAnimator.Instance?.PlayAnimation(_currentAbilityBase.animationName);


                    //caculate when the aiblity is over
                    _abilityTimer = Observable.EveryUpdate().First()
                        .Delay(TimeSpan.FromSeconds(_currentAbilityBase.lifeTime))
                        .Subscribe(_ => { ChangeAbility(AbilityType.None); }).AddTo(this);

                    Debug.Log(_currentAbilityBase.name);
                    return;
                }
            }

            //if the ability is null the change to abilityType none
            Debug.LogError("null current ability");
            ChangeAbility(AbilityType.None);
        }


        private void OnTriggerEnter(Collider other)
        {
            // if (currentAbility != AbilityType.None) return;
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
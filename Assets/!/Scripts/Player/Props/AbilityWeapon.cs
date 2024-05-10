using System;
using System.Collections.Generic;
using _.Scripts.Ability;
using _.Scripts.Enemy;
using _.Scripts.Event;
using _.Scripts.Interface;
using _.Scripts.Player.Ability;
using DG.Tweening;
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
            Key,
            BreakWall,
            Gun,
        }

        [SerializeField, Header("Put the sword model to here")]
        private GameObject swordTransform;

        [FormerlySerializedAs("abilityBase")]
        [Tooltip("Put all the ability scriptable object to list")]
        [SerializeField]
        private List<AbilityBase> abilityBaseList = new List<AbilityBase>();

        public AbilityBase currentAbilityBase;

        public AbilityType currentAbility = AbilityType.None;

        [SerializeField] private GameObject[] inMouthObjectObjectList;

        public IDisposable _abilityTimer;
        public IDisposable _QuitInMouthObjectObjectTimer;

        public float currentAbilityTime;

        [SerializeField] private AbilityValueUI _abilityValueUI;
        
        public static Action onPlayerGetAbility;
        public static Action onPlayerQuitAbility;


        private void Start()
        {
            transform.parent = swordTransform.transform;
            ChangeAbility(AbilityType.None);
        }

        private void Update()
        {
            //for test
            if (Input.GetKeyDown(KeyCode.Z)) ChangeAbility(AbilityType.None);

            if (Input.GetKeyDown(KeyCode.X)) ChangeAbility(AbilityType.Strength);

            if (Input.GetKeyDown(KeyCode.C)) ChangeAbility(AbilityType.BreakWall);

            if (Input.GetKeyDown(KeyCode.V)) ChangeAbility(AbilityType.Key);


            if (Input.GetKeyDown(KeyCode.N)) ChangeAbility(AbilityType.Gun);

            if (Input.GetKeyDown(KeyCode.M)) ChangeAbility(AbilityType.Fire);
        }

        public void ExecuteAblilty()
        {
            currentAbilityBase.AbilityAlgorithm();
        }

        public void ChangeAbility(AbilityType getAbility)
        {
            _abilityTimer?.Dispose();
            _QuitInMouthObjectObjectTimer?.Dispose();
            //means stop showing value UI
            _abilityValueUI.DisplayTime(0, 0);


            if (currentAbilityBase != null)
                currentAbilityBase.QuitAbilityAlgorithm();


//make current ability containers gameObject visible 

            foreach (var abilityObject in inMouthObjectObjectList)
            {
                abilityObject.transform.DOScale(Vector3.one, 0);
            }

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
                    currentAbilityBase = ability;
                    attackValue = currentAbilityBase.damage;
                    attackAction = currentAbilityBase.TriggerEffect;
                    currentAbility = getAbility;
                    currentAbilityBase.StartAbility(this);
                    AbilityWeaponAnimator.Instance?.PlayAnimation(currentAbilityBase.animationName);


                    //caculate when the aiblity is over
                    // _abilityTimer = Observable.EveryUpdate().First()
                    //     .Delay(TimeSpan.FromSeconds(currentAbilityBase.lifeTime))
                    //     .Subscribe(_ => { ChangeAbility(AbilityType.None); }).AddTo(this);

                    if (currentAbilityBase.abilityType == AbilityType.None) return;

                    float keepTime = currentAbilityBase.lifeTime * 10;
                    float addTionTime = 1;
                    _abilityTimer = Observable.Interval(TimeSpan.FromSeconds(0.1f))
                        .TakeWhile(time => time <= keepTime)
                        .Subscribe(time =>
                        {
                            float remainingTime = keepTime - (time * addTionTime);
                            _abilityValueUI.DisplayTime(remainingTime, keepTime);

                            if (remainingTime <= 0)
                            {
                                ChangeAbility(AbilityType.None);

                                _abilityTimer.Dispose(); // 结束计时器
                            }
                        }).AddTo(this);


                    Debug.Log(currentAbilityBase.name);
                    return;
                }
            }

            //if the ability is null the change to abilityType none
            Debug.LogError("null current ability");
            ChangeAbility(AbilityType.None);
        }

        public void QuitInMouthObjectObject()
        {
            Observable.Timer(TimeSpan.FromSeconds(0.5f)).First()
                .Subscribe(_ =>
                {
                    foreach (var abilityObject in inMouthObjectObjectList)
                    {
                        abilityObject.transform.DOScale(Vector3.one * 0.6f, 0.15f);
                    }
                }).AddTo(this);

            _QuitInMouthObjectObjectTimer = Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(0.7f)).First()
                .Subscribe(
                    _ =>
                    {
                        foreach (var abilityObject in inMouthObjectObjectList)
                        {
                            abilityObject.gameObject.SetActive(false);
                        }
                    }).AddTo(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            // if (currentAbility != AbilityType.None) return;
            if (other.gameObject.TryGetComponent(out IAbilityContainer getAbility))
            {
                ChangeAbility(getAbility.GetAbility());
                GetComponent<Collider>().enabled = false;
                // AutoTurnAroundDetect.onRemoveDetectList?.Invoke(other.gameObject);
            }
        }

        private void OnDisable()
        {
            if (currentAbilityBase != null)
                currentAbilityBase.QuitAbilityAlgorithm();
        }
    }
}
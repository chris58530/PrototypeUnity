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
            Ice
        }

        [SerializeField] private List<AbilityBase> abilityBase = new List<AbilityBase>();
        private AbilityBase _currentAbilityBase;

        private void Start()
        {
            ChangeAbility(AbilityType.None);
        }

        private void Update()
        {
            ExecuteAblilty();
            if (Input.GetKeyDown(KeyCode.Z)) ChangeAbility(AbilityType.Strength);
        }


        private void ChangeAbility(AbilityType getAbility)
        {
            if (_currentAbilityBase != null)
                _currentAbilityBase.QuitAbilityAlgorithm();

            foreach (var ability in abilityBase)
            {
                if (ability.abilityType == getAbility)
                {
                    _currentAbilityBase = ability;
                    attackValue = _currentAbilityBase.damage;
                    attackAction = _currentAbilityBase.TriggerEffect;
                    _currentAbilityBase.StartAbility();
                    Observable.EveryUpdate().First()
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

            Debug.Log("null current ability");
            ChangeAbility(AbilityType.None);
        }

        public void ExecuteAblilty()
        {
            _currentAbilityBase.AbilityAlgorithm();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IAbilityContainer getAbility))
            {
                ChangeAbility(getAbility.GetAbility());
            }
        }
    }
}
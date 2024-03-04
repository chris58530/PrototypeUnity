using System;
using _.Scripts.Interface;
using _.Scripts.Player.Props;
using UnityEngine;
using UniRx;

namespace @_.Scripts.Ability
{
    [CreateAssetMenu(fileName = "StrengthAbilityData", menuName = "Ability/StrengthAbility", order = 2)]
    public class StrengthAbilitySO : AbilityBase
    {
        [SerializeField] private Material effect;
        private IDisposable lifeTimer;

        public override void AbilityAlgorithm()
        {
            //Hold this ability will do 


            Debug.Log("Use Strength Ability");
        }

        public override void StartAbility()
        {
            if (effect != null)
                effect.EnableKeyword("_EMISSION");
        }

        public override void QuitAbilityAlgorithm(Transform transform)
        {
            if (effect != null)
                effect.DisableKeyword("_EMISSION");
            //產生對應物件 噴出原始怪物
            Debug.Log("QuitAbilityAlgorithm");
        }

        public override void TriggerEffect(Collider other)
        {
            //OnTrigger enemy will do 
            Debug.Log("Use Strength TriggerEffect ");
            if (other.TryGetComponent<IShieldable>(out IShieldable target))
                target.OnTakeShield(1);
        }

        // private void OnDisable()
        // {
        //     QuitAbilityAlgorithm();
        // }
    }
}
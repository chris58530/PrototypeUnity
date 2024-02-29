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
            // effect = GameObject.Find("StrengthAbilityEffect");
            effect.EnableKeyword("_EMISSION");
        }

        public override void QuitAbilityAlgorithm()
        {
            effect.DisableKeyword("_EMISSION");
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
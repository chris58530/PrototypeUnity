using System;
using _.Scripts.Interface;
using _.Scripts.Player.Props;
using UnityEngine;
using UniRx;
using Unity.VisualScripting;

namespace @_.Scripts.Ability
{
    [CreateAssetMenu(fileName = "StrengthAbilityData", menuName = "Ability/StrengthAbility", order = 2)]
    public class StrengthAbilitySO : AbilityBase
    {
        private GameObject effect;
        private IDisposable lifeTimer;


        public override void AbilityAlgorithm()
        {
            //Hold this ability will do 

            effect.GetComponent<MeshRenderer>().enabled = true;

            Debug.Log("Use Strength Ability");
        }

        public override void StartAbility()
        {
            effect = GameObject.Find("StrengthAbilityEffect");
            effect.GetComponent<MeshRenderer>().enabled = false;
        
        }

        public override void QuitAbilityAlgorithm()
        {
            Debug.Log("QuitAbilityAlgorithm");
            effect.GetComponent<MeshRenderer>().enabled = false;

        }

        public override void TriggerEffect(Collider other)
        {
            //OnTrigger enemy will do 
            Debug.Log("Use Strength TriggerEffect ");
            if (other.TryGetComponent<IShieldable>(out IShieldable target))
                target.OnTakeShield(1);
        }

    }
}
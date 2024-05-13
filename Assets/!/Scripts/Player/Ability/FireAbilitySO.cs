using _.Scripts.Player.Ability;
using _.Scripts.Player.Props;
using UnityEngine;

namespace @_.Scripts.Ability
{
    [CreateAssetMenu(fileName = "FireAbilityData", menuName = "Ability/FireAbility", order = 3)]
    public class FireAbilitySO : AbilityBase
    {
        public override void AbilityAlgorithm()
        {
            //Hold this ability will do 

            Debug.Log("Use fire Ability");

        }

        public override void StartAbility(AbilityWeapon weapon)
        {
            AbilityWeapon.onPlayerGetAbility?.Invoke();

        }

        public override void QuitAbilityAlgorithm()
        {
            AbilityWeapon.onPlayerQuitAbility?.Invoke();

        }
        public override void TriggerEffect(Collider other)
        {
            //OnTrigger enemy will do 
            if (other.gameObject.TryGetComponent<GoblinTorch>(out var target))
            {
                target.OpenTorchLight();
                Debug.Log("Use fire TriggerEffect ");
            }

        }
    }
}
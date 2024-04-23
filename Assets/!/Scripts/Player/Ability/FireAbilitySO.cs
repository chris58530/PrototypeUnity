using _.Scripts.Player.Ability;
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

        public override void StartAbility()
        {
            
        }

        public override void QuitAbilityAlgorithm()
        {

        }
        public override void TriggerEffect(Collider other)
        {
            //OnTrigger enemy will do 
            if (other.gameObject.TryGetComponent<Torch>(out var target))
            {
                target.OpenTorchLight();
                Debug.Log("Use fire TriggerEffect ");
            }

        }
    }
}
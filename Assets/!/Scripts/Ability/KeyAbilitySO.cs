using UnityEngine;

namespace @_.Scripts.Ability
{
    [CreateAssetMenu(fileName = "KeyAbilityData", menuName = "Ability/KeyAbility", order = 5)]

    public class KeyAbilitySO : AbilityBase
    {
        public override void AbilityAlgorithm()
        {
            //Hold this ability will do 

            Debug.Log("Use Key Ability");

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
            Debug.Log("Use Key TriggerEffect ");
        }
    }
}

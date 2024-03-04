using _.Scripts.Player.Props;
using UnityEngine;

namespace _.Scripts.Ability
{
    [CreateAssetMenu(fileName = "NoneAbilityData", menuName = "Ability/NoneAbility", order = 1)]
    public class NoneAbilitySO : AbilityBase
    {
        public override void AbilityAlgorithm()
        {
            //Hold this ability will do 

            Debug.Log("Use None Ability");
        }

        public override void StartAbility()
        {

        }

        public override void QuitAbilityAlgorithm(Transform transform)
        {
            
        }

        public override void TriggerEffect(Collider other)
        {
            //OnTrigger enemy will do 
            Debug.Log("Use None TriggerEffect ");
        }
    }
}
using _.Scripts.Player.Props;
using _.Scripts.Task;
using UnityEngine;

namespace @_.Scripts.Ability
{
    [CreateAssetMenu(fileName = "DashAbilityData", menuName = "Ability/DashAbility", order = 6)]
    public class DashAbilitySO : AbilityBase
    {

        public override void AbilityAlgorithm()
        {
            //Hold this ability will do 

            Debug.Log("Use Dash Ability");
        }

        public override void StartAbility()
        {               
        }

        public override void QuitAbilityAlgorithm()
        {
        }

        public override void TriggerEffect(Collider other)
        {
       
        }
    }
}

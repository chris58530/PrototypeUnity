using UnityEngine;

namespace @_.Scripts.Ability
{
    [CreateAssetMenu(fileName = "FireAbilityData", menuName = "Ability/FireAbility", order = 3)]
    public class FireAbilitySO : AbilityBase
    {
        public override void AbilityAlgorithm()
        {
            Debug.Log("Use fire Ability");

        }
    }
}
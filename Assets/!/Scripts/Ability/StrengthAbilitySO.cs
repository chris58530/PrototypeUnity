using _.Scripts.Player.Props;
using UnityEngine;

namespace @_.Scripts.Ability
{
    [CreateAssetMenu(fileName = "StrengthAbilityData", menuName = "Ability/StrengthAbility", order = 2)]
    public class StrengthAbilitySO : AbilityBase
    {
      
        public override void AbilityAlgorithm()
        {
            Debug.Log("Use Strength Ability");

        }
    }
}
using _.Scripts.Player.Props;
using UnityEngine;

namespace _.Scripts.Ability
{[CreateAssetMenu(fileName = "NoneAbilityData", menuName = "Ability/NoneAbility", order = 1)]
    public class NoneAbilitySO:AbilityBase
    {

        public override void AbilityAlgorithm()
        {
            Debug.Log("Use None Ability");
        }
    }
}
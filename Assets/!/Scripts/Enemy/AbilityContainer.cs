using _.Scripts.Interface;
using _.Scripts.Player.Props;
using UnityEngine;
using UnityEngine.Serialization;

namespace _.Scripts.Enemy
{
    public class AbilityContainer : MonoBehaviour, IAbilityContainer
    {
       public AbilityWeapon.AbilityType abilityType;

        public AbilityWeapon.AbilityType GetAbility()
        {
            return abilityType;
        }
    }
}
using _.Scripts.Interface;
using _.Scripts.Player.Props;
using UnityEngine;

namespace _.Scripts.Enemy
{
    public class AbilityContainer : MonoBehaviour, IAbsorbable
    {
        public SwordAbility.Ability ability;

        public SwordAbility.Ability ReturnAbility()
        {
            return ability;
        }
    }
}
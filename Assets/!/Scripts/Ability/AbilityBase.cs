using System;
using _.Scripts.Player.Props;
using UnityEngine;
using UnityEngine.Serialization;

namespace @_.Scripts.Ability
{
    [Serializable]
    public abstract class AbilityBase : ScriptableObject
    {
         public AbilityWeapon.AbilityType abilityType;

        public int damage;
        public abstract void AbilityAlgorithm ();
    }
}

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
        public int lifeTime;
        public abstract void AbilityAlgorithm ();
        public abstract void StartAbility ();
        public abstract void QuitAbilityAlgorithm();
        public abstract void TriggerEffect(Collider other);
    }
}

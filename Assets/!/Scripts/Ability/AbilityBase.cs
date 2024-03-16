using System;
using _.Scripts.Player.Props;
using UnityEngine;

namespace @_.Scripts.Ability
{
    [Serializable]
    public abstract class AbilityBase : ScriptableObject
    {
        public AbilityWeapon.AbilityType abilityType;
        public AbilityWeaponAnimator.AnimationName animationName;
        public int damage;
        public int lifeTime;
        public GameObject inMouthObject;

        public abstract void AbilityAlgorithm();
        public abstract void StartAbility();
        public abstract void QuitAbilityAlgorithm();
        public abstract void TriggerEffect(Collider other);
    }
}
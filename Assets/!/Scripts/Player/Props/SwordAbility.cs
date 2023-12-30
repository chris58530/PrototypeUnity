using System;
using _.Scripts.Interface;
using UnityEngine;
using UnityEngine.Serialization;

namespace @_.Scripts.Player.Props
{
    public class SwordAbility : MonoBehaviour
    {
        public enum Ability
        {
            None,
            Strength,
            Fire,
            Ice
        }

        public Ability swordAbility;

        private void Start()
        {
            swordAbility = Ability.None;
        }

        public void ChangeAbility(Ability ability)
        {
            swordAbility = ability;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IAbsorbable getAbility))
            {
                ChangeAbility(getAbility.ReturnAbility());
            }
        }
    }
}
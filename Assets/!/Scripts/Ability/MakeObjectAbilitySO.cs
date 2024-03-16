using System;
using _.Scripts.Interface;
using UnityEngine;
using UniRx;

namespace @_.Scripts.Ability
{
    [CreateAssetMenu(fileName = "MakeObjectAbilityData", menuName = "Ability/MakeObjectAbility", order = 3)]

    public class MakeObjectAbilitySO : AbilityBase
    {
        private GameObject effect;
        private GameObject spawnObject;
        private IDisposable lifeTimer;


        public override void AbilityAlgorithm()
        {
            //Hold this ability will do 
            effect.GetComponent<MeshRenderer>().enabled = true;

         
        }

        public override void StartAbility()
        {
        
        }

        public override void QuitAbilityAlgorithm()
        {
            effect.GetComponent<MeshRenderer>().enabled = false;


        }

        public override void TriggerEffect(Collider other)
        {
            //OnTrigger enemy will do 
          
        }

    }
}

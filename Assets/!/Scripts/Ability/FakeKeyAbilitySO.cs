using _.Scripts.Player.Props;
using _.Scripts.Task;
using UnityEngine;

namespace @_.Scripts.Ability
{
    [CreateAssetMenu(fileName = "FakeKeyAbilityData", menuName = "Ability/FakeKeyAbility", order = 6)]
    public class FakeKeyAbilitySO : AbilityBase
    {
        public GameObject fakeKeyMonster;

        public override void AbilityAlgorithm()
        {
            //Hold this ability will do 

            Debug.Log("Use Fake Key Ability");
        }

        public override void StartAbility()
        {               
        }

        public override void QuitAbilityAlgorithm(Transform transform)
        {
            // Instantiate(fakeKeyMonster, transform.position, transform.rotation);
        }

        public override void TriggerEffect(Collider other)
        {
            //OnTrigger enemy will do 
       
        }
    }
}

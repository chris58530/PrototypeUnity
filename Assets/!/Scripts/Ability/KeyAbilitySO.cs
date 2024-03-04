using _.Scripts.Player.Props;
using _.Scripts.Task;
using UnityEngine;

namespace @_.Scripts.Ability
{
    [CreateAssetMenu(fileName = "KeyAbilityData", menuName = "Ability/KeyAbility", order = 5)]
    public class KeyAbilitySO : AbilityBase
    {
        public GameObject keyMonster;
        public int taskNumber;

        public override void AbilityAlgorithm()
        {
            //Hold this ability will do 

            Debug.Log("Use Key Ability");
        }

        public override void StartAbility()
        {
        }

        public override void QuitAbilityAlgorithm(Transform transform)
        {
            Instantiate(keyMonster, transform.position, transform.rotation);
        }

        public override void TriggerEffect(Collider other)
        {
            //OnTrigger enemy will do 
            if (other.gameObject.CompareTag("KeyDoor"))
            {
                Debug.Log("Use Key TriggerEffect ");

                TaskManager.checkTaskAction?.Invoke(taskNumber);
                AbilityWeapon weapon = FindObjectOfType<AbilityWeapon>();
                weapon.ChangeAbility(AbilityWeapon.AbilityType.None);
            }
        }
    }
}
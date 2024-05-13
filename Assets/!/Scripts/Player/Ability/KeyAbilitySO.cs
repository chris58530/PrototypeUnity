using _.Scripts.Player.Ability;
using _.Scripts.Player.Props;
using _.Scripts.Task;
using UnityEngine;

namespace @_.Scripts.Ability
{
    [CreateAssetMenu(fileName = "KeyAbilityData", menuName = "Ability/KeyAbility", order = 5)]
    public class KeyAbilitySO : AbilityBase
    {
        private AbilityWeapon _abilityWeapon;

        public override void AbilityAlgorithm()
        {
            //Hold this ability will do 

            Debug.Log("Use Key Ability");
        }

        public override void StartAbility(AbilityWeapon weapon)
        {            AbilityWeapon.onPlayerGetAbility?.Invoke();

            _abilityWeapon = weapon;
        }

        public override void QuitAbilityAlgorithm()
        {
            AbilityWeapon.onPlayerQuitAbility?.Invoke();

            // Instantiate(keyMonster, transform.position, transform.rotation);
        }

        public override void TriggerEffect(Collider other)
        {
            //OnTrigger enemy will do 
            if (other.gameObject.TryGetComponent<KeyDoor>(out var door))
            {
                door.DoResult();
                _abilityWeapon.ChangeAbility(AbilityWeapon.AbilityType.None);
                //
                // TaskManager.checkTaskAction?.Invoke(taskNumber);
                // AbilityWeapon weapon = FindObjectOfType<AbilityWeapon>();
                // weapon.ChangeAbility(AbilityWeapon.AbilityType.None);
            }
        }
    }
}
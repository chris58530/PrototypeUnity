using _.Scripts.Interface;
using _.Scripts.Player.Props;
using UnityEngine;
using UnityEngine.Events;


namespace _.Scripts.Enemy
{
    public class AbilityContainer : MonoBehaviour, IAbilityContainer
    {
        public AbilityWeapon.AbilityType abilityType;

        public bool canGetAbilty;
        [SerializeField] private UnityEvent onGetAbiltyEvent;


        public AbilityWeapon.AbilityType GetAbility()
        {
            if (!canGetAbilty)
                return AbilityWeapon.AbilityType.None;

            onGetAbiltyEvent?.Invoke();
            return abilityType;
        }

//for check showing model not for change ability data SO
        public AbilityWeapon.AbilityType CheckAbility()
        {
            return abilityType;
        }

        public void SetCanGetAbility(bool canGet)
        {
            canGetAbilty = canGet;
        }

        public void DestroyGameObject(GameObject obj)
        {
            Destroy(obj);
        }
    }
}
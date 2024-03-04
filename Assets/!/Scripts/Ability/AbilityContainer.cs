using _.Scripts.Interface;
using _.Scripts.Player.Props;
using UnityEngine;
using UnityEngine.Serialization;

namespace _.Scripts.Enemy
{
    public class AbilityContainer : MonoBehaviour, IAbilityContainer
    {
        public AbilityWeapon.AbilityType abilityType;
        [SerializeField] private GameObject afterDestroyObject;
        [SerializeField] private Vector3 offSet;
        [SerializeField] private bool canGetAbilty;

        public AbilityWeapon.AbilityType GetAbility()
        {
            if (!canGetAbilty)
                return AbilityWeapon.AbilityType.None;

            FadeOut();
            return abilityType;
        }

        public void SetCanGetAbility()
        {
            canGetAbilty = true;
        }

        void FadeOut()
        {
            if (afterDestroyObject != null)
                Instantiate(afterDestroyObject, transform.position + offSet, transform.rotation);
            Destroy(gameObject);
        }
    }
}
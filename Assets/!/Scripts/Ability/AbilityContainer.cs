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

        public AbilityWeapon.AbilityType GetAbility()
        {
            FadeOut();

            return abilityType;
        }

        void FadeOut()
        {
            Instantiate(afterDestroyObject, transform.position + offSet, transform.rotation);
            Destroy(gameObject);
        }
    }
}
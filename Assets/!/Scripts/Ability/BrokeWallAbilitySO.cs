using UnityEngine;

namespace @_.Scripts.Ability
{
    [CreateAssetMenu(fileName = "BrokeWallAbilityData", menuName = "Ability/BrokeWallAbility", order = 7)]
    public class BrokeWallAbilitySO : AbilityBase
    {
        public override void AbilityAlgorithm()
        {
            //Hold this ability will do 

            Debug.Log("Use Broke Wall Ability");
        }

        public override void StartAbility()
        {
            GameObject.Find("AttackCollider").tag = "RemoveShield";
        }

        public override void QuitAbilityAlgorithm(Transform transform)
        {
            // Instantiate(fakeKeyMonster, transform.position, transform.rotation);
            GameObject.Find("AttackCollider").tag = "Sword";
        }

        public override void TriggerEffect(Collider other)
        {
            if (other.gameObject.TryGetComponent<StoneDoor>(out var door))
            {
            }
        }
    }
}
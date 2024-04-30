using _.Scripts.Interface;
using _.Scripts.Player.Ability;
using _.Scripts.Player.Props;
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

        public override void StartAbility(AbilityWeapon weapon)
        {
            GameObject.Find("AttackCollider").GetComponent<AttackWeapon>().AddLayerFromMask(true,"Breakable");
        }

        public override void QuitAbilityAlgorithm()
        {
            // Instantiate(fakeKeyMonster, transform.position, transform.rotation);
            GameObject.Find("AttackCollider").GetComponent<AttackWeapon>().AddLayerFromMask(false,"Breakable");

            Debug.Log($"------{this.name} QuitAbilityAlgorithm------");

        }

        public override void TriggerEffect(Collider other)
        {
            if (other.TryGetComponent<IBreakable>(out IBreakable target))
                target.OnTakeAttack();
            
        }
    }
}


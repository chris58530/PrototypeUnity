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
        }

        public override void StartAbility(AbilityWeapon weapon)
        {
            GameObject.Find("PlayerAttackCollider").GetComponent<AttackWeapon>().AddLayerFromMask(true, "Breakable");
            AbilityWeapon.onPlayerGetAbility?.Invoke();
        }

        public override void QuitAbilityAlgorithm()
        {
            // Instantiate(fakeKeyMonster, transform.position, transform.rotation);
            AbilityOnFire.onFire?.Invoke(false);

            GameObject.Find("PlayerAttackCollider").GetComponent<AttackWeapon>().AddLayerFromMask(false, "Breakable");
        }

        public override void TriggerEffect(Collider other)
        {
            if (other.TryGetComponent<IBreakable>(out IBreakable target))
                target.OnTakeBreakableAttack();
            
            if (other.gameObject.TryGetComponent<CampFire>(out var campFire))
            {
                AbilityOnFire.onFire?.Invoke(true);
            }

            if (other.gameObject.TryGetComponent<Torch>(out var torch))
            {
                if (AbilityOnFire.isOnFire)
                {
                    torch.OpenTorchLight();
                }
            }
        }
    }
}
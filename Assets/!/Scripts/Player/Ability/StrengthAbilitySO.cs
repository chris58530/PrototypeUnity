using System;
using _.Scripts.Interface;
using _.Scripts.Player.Ability;
using _.Scripts.Player.Props;
using UnityEngine;
using UniRx;

namespace @_.Scripts.Ability
{
    [CreateAssetMenu(fileName = "StrengthAbilityData", menuName = "Ability/StrengthAbility", order = 2)]
    public class StrengthAbilitySO : AbilityBase
    {
        private GameObject powerfulSword;
        private IDisposable lifeTimer;
        private SwordSlash sword;

        public override void AbilityAlgorithm()
        {
            //Hold this ability will do 
            if (Input.GetKeyDown(KeyCode.F))
            {
                AbilityOnFire.onFire?.Invoke(true);
            }
        }

        public override void StartAbility(AbilityWeapon weapon)
        {
            AbilityWeapon.onPlayerGetAbility?.Invoke();
            powerfulSword = GameObject.Find("NoChargeSword");
            LemonBase.onUseBTSpeak?.Invoke(LemonSpeakEnum.InsertCrystal);

            sword = FindObjectOfType<SwordSlash>();
            sword.canUse = true;
            if (powerfulSword != null)
                powerfulSword.SetActive(false);
        }

        public override void QuitAbilityAlgorithm()
        {
            if (powerfulSword != null)
                powerfulSword.SetActive(true);
            sword.canUse = false;
            AbilityOnFire.onFire?.Invoke(false);
            AbilityWeapon.onPlayerQuitAbility?.Invoke();
            AudioManager.Instance.PlaySFX("OutOfMana");

        }

        public override void TriggerEffect(Collider other)
        {
            //OnTrigger enemy will do 
            if (other.TryGetComponent<IShieldable>(out IShieldable target))
                target.OnTakeShield(1);

            if (other.gameObject.TryGetComponent<CampFire>(out var campFire))
            {
                AbilityOnFire.onFire?.Invoke(true);
            }

            if (other.gameObject.TryGetComponent<GoblinTorch>(out var torch))
            {
                if (AbilityOnFire.isOnFire)
                {
                    torch.OpenTorchLight();
                }
            }
        }

        // private void OnDisable()
        // {
        //     QuitAbilityAlgorithm();
        // }
    }
}
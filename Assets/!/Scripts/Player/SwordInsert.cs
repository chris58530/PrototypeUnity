using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Player;
using _.Scripts.Player.Props;
using UnityEngine;
using UnityEngine.Serialization;

public class SwordInsert : MonoBehaviour
{
    [FormerlySerializedAs("abilityValue")] [SerializeField] private AbilityValueUI abilityValueUI;
    [SerializeField] private AbilityWeapon abilityWeapon;

    private void Update()
    {
        DicreaseSwordStrengthAbilityTime();
    }

    public void AddSwordAbilityTime()
    {
        //每秒 -deltaTime 因此乘上兩倍
        if (abilityWeapon.currentAbilityBase.abilityType != AbilityWeapon.AbilityType.Strength) return;

        abilityWeapon._abilityTimer?.Dispose();

        if (abilityWeapon.currentAbilityTime < abilityWeapon.currentAbilityBase.lifeTime)
            abilityWeapon.currentAbilityTime += Time.deltaTime * 8;
    }

    public void DicreaseSwordStrengthAbilityTime()
    {
        if (abilityWeapon.currentAbilityBase == null) return;

        if (abilityWeapon.currentAbilityBase.abilityType != AbilityWeapon.AbilityType.Strength) return;

        if (abilityWeapon.currentAbilityTime >= 0)
        {
            abilityWeapon.currentAbilityTime -= Time.deltaTime;
            abilityValueUI.DisplayTime(abilityWeapon.currentAbilityTime, 10);

        }
        else
        {
            abilityWeapon.ChangeAbility(AbilityWeapon.AbilityType.None);
            abilityValueUI.StopUpdateTime();
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Crystal>(out var crystal))
        {
            AddSwordAbilityTime();
        }
    }
}
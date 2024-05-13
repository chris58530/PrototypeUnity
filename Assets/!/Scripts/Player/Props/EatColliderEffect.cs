using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Player.Props;
using DG.Tweening;
using UnityEngine;

public class EatColliderEffect : MonoBehaviour
{
    [SerializeField] private Material shinymat;
    [SerializeField] private int intensity; //攻擊時發亮程度
    private readonly float duration = 1;

    private void OnEnable()
    {
        QuitAbilityEmission();
        AbilityWeapon.onPlayerGetAbility += GetAbilityEmission;
        AbilityWeapon.onPlayerQuitAbility += QuitAbilityEmission;
    }

    private void OnDisable()
    {
        QuitAbilityEmission();

        AbilityWeapon.onPlayerGetAbility -= GetAbilityEmission;
        AbilityWeapon.onPlayerQuitAbility -= QuitAbilityEmission;
    }

    void GetAbilityEmission()
    {
        shinymat.DOColor(new Color(1, 1, 1) * intensity, "_EmissionColor",
            duration); // Transition to the new intensity over t
    }

    void QuitAbilityEmission()
    {
        shinymat.DOColor(new Color(0, 0, 0), "_EmissionColor", duration); // Transition to the new intensity over t
    }
}
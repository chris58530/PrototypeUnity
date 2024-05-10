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
        QuitAbility();
        AbilityWeapon.onPlayerGetAbility += GetAbility;
        AbilityWeapon.onPlayerQuitAbility += QuitAbility;
    }

    private void OnDisable()
    {
        QuitAbility();

        AbilityWeapon.onPlayerGetAbility -= GetAbility;
        AbilityWeapon.onPlayerQuitAbility -= QuitAbility;
    }

    void GetAbility()
    {
        shinymat.DOColor(new Color(1, 1, 1) * intensity, "_EmissionColor",
            duration); // Transition to the new intensity over t
    }

    void QuitAbility()
    {
        shinymat.DOColor(new Color(0, 0, 0), "_EmissionColor", duration); // Transition to the new intensity over t
    }
}
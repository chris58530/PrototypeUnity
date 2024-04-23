using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Tools;
using UnityEngine;

public class AbilityWeaponAnimator : Singleton<AbilityWeaponAnimator>
{
    public enum AnimationName
    {
        aIdle,
        Q1,
        Q2,
        Q3,
        Azbsword,
        Hold_Big,
        Hold_Medium,
        Hold_Small,
        
        Swallow
    }

    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }


    public void PlayAnimation(AnimationName name)
    {
        if (animator == null) return;
        animator.Play(name.ToString());
    }
}
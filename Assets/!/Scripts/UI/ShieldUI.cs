using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUI : MonoBehaviour
{
    [SerializeField] private Animator _ani;

    private void Awake()
    {
    }

    public void HitShield()
    {
        //shake all shield ui
        
        _ani.Play("ShieldShaking");

    }

    public void BreakShield(int shieldNumber)
    {
        //play current shield break animaion 
        //disactive current shield
        _ani.Play("ShieldBreaking");
    }
}
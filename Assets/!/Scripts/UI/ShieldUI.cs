using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldUI : MonoBehaviour
{
    [SerializeField] private Animator[] shieldAni;
    private Animator _thisAni;

    private void Awake()
    {
        _thisAni = GetComponent<Animator>();
    }

    public void HitShield()
    {
        //shake all shield ui
        _thisAni.Play("ShieldShaking");
    }

    public void BreakShield(int shieldNumber)
    {
        //play current shield break animaion 
        //disactive current shield
        if (shieldNumber < 0) return;
        shieldAni[shieldNumber].Play("ShieldBreaking");
    }

    public void ResetShield()
    {
        for (int i = 0; i < shieldAni.Length; i++)
        {
            shieldAni[i].Play("ResetShield");
        }
    }
}
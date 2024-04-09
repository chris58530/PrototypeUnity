using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShieldUI : MonoBehaviour
{
    [SerializeField] private Animator[] shieldAni;
    private Animator _thisAni;

    private void Awake()
    {
        _thisAni = GetComponent<Animator>();
    }

    public void HitShield(int shieldNumber)
    {
        //shake all shield ui

        Debug.Log("hit shield animatiob");
        for (int i = 0; i < shieldAni.Length; i++)
        {
            if (shieldNumber > i)
            {
                shieldAni[i].Play("ShieldShaking");
            }
        }

        // StartCoroutine(ShakeAnimatorObjec());
    }

    public void DisableImage()
    {
        for (int i = 0; i < shieldAni.Length; i++)
        {
            shieldAni[i].Play("None");
        }
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
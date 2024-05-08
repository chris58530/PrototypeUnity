using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class HandEffect : MonoBehaviour
{
    //模型總共有三個 依序開啟
    [Header("Attack Setting")]
    [SerializeField] private Material[] gemMaterials;
    [Range(0.1f, 10f)]
    [SerializeField] private float duration;
    [Range(0, 30)]
    [SerializeField]private int intensity;//攻擊時發亮程度
    
    [Header("Break Setting")]
    [SerializeField] private GameObject[] originObjects;
    [SerializeField] private GameObject[] break1Objects;
    [SerializeField] private GameObject[] break2Objects;
    [SerializeField] private ParticleSystem breakEffect; //護頓特效
    bool isBreak1; //是否正在護頓中
    bool isBreak2; //是否正在護頓中

    public void SwitchBreakMaterial(BreakState breakState)
    {
        if (breakState == BreakState.Break1)
        {
            if (!isBreak1)
            {
                isBreak1 = true;
                breakEffect.Play();
            }

            //除了break1Objects都關掉 break1Objects打開
            for (int i = 0; i < originObjects.Length; i++)
            {
                originObjects[i].SetActive(false);
            }

            for (int i = 0; i < break1Objects.Length; i++)
            {
                break1Objects[i].SetActive(true);
            }

            for (int i = 0; i < break2Objects.Length; i++)
            {
                break2Objects[i].SetActive(false);
            }
        }

        if (breakState == BreakState.Break2)
        {
            if (!isBreak2)
            {
                isBreak2 = true;
                breakEffect.Play();
            }

            for (int i = 0; i < originObjects.Length; i++)
            {
                originObjects[i].SetActive(false);
            }

            for (int i = 0; i < break1Objects.Length; i++)
            {
                break1Objects[i].SetActive(false);
            }

            for (int i = 0; i < break2Objects.Length; i++)
            {
                break2Objects[i].SetActive(true);
            }
        }
    }

    public void SetMaterialsEmission(bool enable)
    {
        if (!enable)
        {
            foreach (var mat in gemMaterials)
                mat.DOColor(new Color(1, 1, 1) , "_EmissionColor",
                    1); 
            return;
        }
        foreach (var mat in gemMaterials)
        {
            mat.DOColor(new Color(1, 1, 1) * intensity, "_EmissionColor",
                duration); // Transition to the new intensity over the specified duration
        }
    }

    private void OnEnable()
    {
        foreach (var mat in gemMaterials)
            mat.SetColor("_EmissionColor", new Color(1, 1, 1));
    }

    private void OnDisable()
    {
        foreach (var mat in gemMaterials)
            mat.SetColor("_EmissionColor", new Color(1, 1, 1));
    }
}
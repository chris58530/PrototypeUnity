using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GroundGemEffect : MonoBehaviour
{
    [SerializeField] private Material[] gemMaterials;

    [Range(0.1f, 10f)]
    [SerializeField] private float duration;
    [Range(0, 10)]
    [SerializeField]private int intensity;
    private bool _isIncreasing = true;

    private void Start()
    {
        ResetEmission();

         DoIntensityTransition();
    }

    public void ResetEmission()
    {
        foreach (var mat in gemMaterials)
            mat.SetColor("_EmissionColor", new Color(1, 1, 1));
    }

    public void TransitionToNewIntensity(int targetIntensity)
    {
        foreach (var mat in gemMaterials)
        {
            mat.DOColor(new Color(1, 1, 1) * targetIntensity, "_EmissionColor",
                duration); // Transition to the new intensity over the specified duration
        }
    }
    private void DoIntensityTransition()
    {
        float targetIntensity = _isIncreasing ? intensity : 0f;

        foreach (var mat in gemMaterials)
        {
            mat.DOColor(new Color(1, 1, 1) * targetIntensity, "_EmissionColor", duration)
                .OnComplete(() =>
                {
                    _isIncreasing = !_isIncreasing;
                    DoIntensityTransition();
                });
        }
    }
    private void OnDisable()
    {
        ResetEmission();


    }
}
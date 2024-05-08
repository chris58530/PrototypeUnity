using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GemEffect : MonoBehaviour
{
    [SerializeField] private Material[] gemMaterials;


    private int currentIntensity;

    private void Start()
    {
        SetEmission(0);

        TransitionToNewIntensity(10, 1);
    }

    public void SetEmission(int intensity)
    {
        foreach (var mat in gemMaterials)
            mat.SetColor("_EmissionColor", new Color(1, 1, 1) * intensity);
    }

    public void TransitionToNewIntensity(int targetIntensity, float duration)
    {
        foreach (var mat in gemMaterials)
        {
            mat.DOColor(new Color(1, 1, 1) * targetIntensity, "_EmissionColor",
                duration); // Transition to the new intensity over the specified duration
        }
    }

    private void OnDisable()
    {
        SetEmission(0);

    }
}
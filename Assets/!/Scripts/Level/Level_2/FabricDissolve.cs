using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using UnityEngine;
using UnityEngine.Serialization;

public class FabricDissolve : MonoBehaviour
{
    [SerializeField] private Renderer[] renderers;

    private void OnDisable()
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.SetFloat("__Surface_Dissolove", -1);
    }

    

    private void OnEnable()
    {

        StartCoroutine(TransitionFloatValue(-1, 1, 2));
    }

    IEnumerator TransitionFloatValue(float startValue, float endValue, float duration)
    {
        float timer = 0.0f;

        while (timer < duration)
        {
            float currentValue = Mathf.Lerp(startValue, endValue, timer / duration);
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.SetFloat("__Surface_Dissolove", currentValue);
            }


            timer += Time.deltaTime;

            yield return null;
        }
    }
}
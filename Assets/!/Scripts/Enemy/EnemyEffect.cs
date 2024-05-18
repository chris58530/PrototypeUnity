using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class EnemyEffect : MonoBehaviour
{
    [Header("Set Self Effect")] [SerializeField]
    private Material EmssionMat;

    [SerializeField] private Material OringinMat;
    [SerializeField] private Renderer[] _renderers;

    [SerializeField] private ParticleSystem stunParticle;
    [SerializeField] private ParticleSystem absortableEffcet;

    private void Start()
    {
        if (stunParticle != null)
            stunParticle.Stop();
        if (absortableEffcet != null)
            absortableEffcet.Stop();
    }

    public void SetEmission()
    {
        foreach (var i in _renderers)
        {
            i.material = EmssionMat;
        }

        Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(0.15f)).First().Subscribe(_ =>
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material = OringinMat;
            }
        }).AddTo(this);
    }

    public void DieMaterialDissolve()
    {
        stunParticle.Stop();

        StartCoroutine(TransitionFloatValue(-1, 1, 2));
    }

    public void OnStun()
    {
        stunParticle.Play();
        absortableEffcet.Play();
    }

    public void OnDead()
    {
        stunParticle.Stop();
        absortableEffcet.Stop();
    }

    IEnumerator TransitionFloatValue(float startValue, float endValue, float duration)
    {
        float timer = 0.0f;

        while (timer < duration)
        {
            float currentValue = Mathf.Lerp(startValue, endValue, timer / duration);
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material.SetFloat("__Surface_Dissolove", currentValue);
            }


            timer += Time.deltaTime;

            yield return null;
        }
    }
}
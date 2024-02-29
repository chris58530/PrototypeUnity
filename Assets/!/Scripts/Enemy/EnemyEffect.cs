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
    [SerializeField] private Renderer Body;
    [SerializeField] private Renderer Neck;
    [SerializeField] private Renderer Weapon;

    public void SetEmission()
    {
        Body.material = EmssionMat;
        Neck.material = EmssionMat;
        Weapon.material = EmssionMat;
        Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(0.2f)).First().Subscribe(_ =>
        {
            Body.material = OringinMat;
            Neck.material = OringinMat;
            Weapon.material = OringinMat;
        }).AddTo(this);
    }

    public void DieMaterialDissolve()
    {
        StartCoroutine(TransitionFloatValue(-1, 1, 2));

    }
    IEnumerator TransitionFloatValue(float startValue, float endValue, float duration)
    {
        float timer = 0.0f;

        while (timer < duration)
        {
            float currentValue = Mathf.Lerp(startValue, endValue, timer / duration);

            Body.material.SetFloat("__Surface_Dissolove", currentValue);
            Neck.material.SetFloat("__Surface_Dissolove", currentValue);
            Weapon.material.SetFloat("__Surface_Dissolove", currentValue);

            timer += Time.deltaTime;

            yield return null;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;
using UniRx;

public class SwordSlash : MonoBehaviour
{
    [SerializeField] private GameObject[] effectObjects;
    private VisualEffect[] _effect;
    [SerializeField] private float[] delayTime;

    public bool canUse;

    public void UseSlash(int count, float scale)
    {
        if (!canUse) return;
        effectObjects[count].transform.localScale = new Vector3(scale, scale, scale);

        Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(delayTime[count])).First().Subscribe(_ =>
        {
            effectObjects[count].GetComponent<VisualEffect>().Play();
        }).AddTo(this);
    }

    private void OnEnable()
    {
        PlayerActions.onPlayerAttackEffect += UseSlash;
    }

    private void OnDisable()
    {
        PlayerActions.onPlayerAttackEffect -= UseSlash;
    }
}
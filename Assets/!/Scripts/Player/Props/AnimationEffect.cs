using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using UniRx;
using UnityEngine;

public class AnimationEffect : MonoBehaviour
{
    private Animator _ani;

    [SerializeField] private float slowSpeed;
    [SerializeField] private float keepTime;

    private void Start()
    {
        _ani = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerActions.onHitEnemy += SlowAnimaiotnSpeed;
    }

    private void OnDisable()
    {
        PlayerActions.onHitEnemy -= SlowAnimaiotnSpeed;
    }

    void SlowAnimaiotnSpeed(float time)
    {
        _ani.speed = slowSpeed;
        Time.timeScale = slowSpeed;
        Debug.Log("slow aniamtion");
        Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(keepTime)).Subscribe(_ =>
        {
            _ani.speed = 1;
            Time.timeScale = 1;
        }).AddTo(this);
    }
}
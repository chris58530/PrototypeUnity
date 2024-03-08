using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Enemy.BossA;
using UnityEngine;

public class BossA_UI : MonoBehaviour
{
    private Animator _ani;

    private void Awake()
    {
        _ani = GetComponent<Animator>();
    }

    void RaiseTowerAnimation()
    {
        _ani.Play("FadeOut");
    }

    void DownTowerAnimation()
    {
        _ani.Play("FadeIn");
    }

    private void OnEnable()
    {
        BossAController.onRaiseTower += RaiseTowerAnimation;
        BossAController.onDownTower += DownTowerAnimation;
    }

    private void OnDisable()
    {
        BossAController.onRaiseTower -= RaiseTowerAnimation;
        BossAController.onDownTower -= DownTowerAnimation;
    }
}
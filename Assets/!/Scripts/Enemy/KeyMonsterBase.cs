using System;
using _.Scripts.Enemy;
using _.Scripts.Interface;
using _.Scripts.Player.Props;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


public class KeyMonsterBase : Enemy, IDamageable, ITaskResult
{
    public Image hpImage;

    [SerializeField] private float maxHp;
    private ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();

    private void Start()
    {
        Initialize();
        _currentHp.Subscribe(_ => { hpImage.fillAmount = _currentHp.Value / maxHp; }).AddTo(this);
    }

    void Initialize()
    {
        _currentHp.Value = maxHp;
    }

    public void OnTakeDamage(int value)
    {
        bt.SendEvent("GetHurt");

    }

    public void OnDied()
    {
        bt.SendEvent("OnDied");
    }

    public void DoResult()
    {
        OnDied();
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}
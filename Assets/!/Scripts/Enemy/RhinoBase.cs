using System;
using _.Scripts.Enemy;
using _.Scripts.Interface;
using BehaviorDesigner.Runtime;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RhinoBase : Enemy, IDamageable, IShieldable
{
    [Tooltip("GROUND OR PLANE MUST BE SET GROUMD LAYER")] [SerializeField]
    private bool isShield = true;

    public Image hpImage;

    [SerializeField] private float maxHp;
    private ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();
    [SerializeField] private UnityEvent onTakeDamagedEvent;
    [SerializeField] private UnityEvent onStunEvent;
    [SerializeField] private UnityEvent onDiedEvent;
    private ShieldUI _shieldUI;

    protected override void Awake()
    {
        base.Awake();
        _shieldUI = GetComponentInChildren<ShieldUI>();
    }

    private void Start()
    {
        Initialize();
        // _currentHp.Subscribe(_ => { hpImage.fillAmount = _currentHp.Value / maxHp; }).AddTo(this);
        BossABomb.bossABigBombEvent += BossABigBombDie;
    }

    void Initialize()
    {
        _currentHp.Value = maxHp;
    }

    public void OnTakeDamage(int value)
    {
        onTakeDamagedEvent?.Invoke();
        bt.SendEvent("OnTakeDamage");

        if (isShield)
        {
           
            _shieldUI.HitShield();
            return;
        }


        if (_currentHp.Value <= 0)
        {
            OnDied();
            return;
        }

        _currentHp.Value -= value;
        if (_currentHp.Value <= 0)
        {
            OnStun();
            onStunEvent?.Invoke();
        }
    }

    public void OnDied()
    {
        onDiedEvent?.Invoke();

        bt.SendEvent("OnDied");
    }

    public void OnStun()
    {
        bt.SendEvent("OnStun");
    }


    public void OnTakeShield(int removeValue)
    {
        _shieldUI.BreakShield(0);
        SharedBool _isShield = false;
        Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(0)).Subscribe(_ => { bt.SetVariable("isShield", _isShield); })
            .AddTo(this);
        isShield = false;
    }

    public void BossABigBombDie()
    {
        // bt.SendEvent("OnDied");

        Destroy(gameObject);
    }

    private void OnDisable()
    {
        BossABomb.bossABigBombEvent -= BossABigBombDie;
    }
}
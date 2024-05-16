using System;
using _.Scripts.Enemy;
using _.Scripts.Interface;
using BehaviorDesigner.Runtime;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RhinoBase : Enemy, IDamageable, IShieldable
{
    [Tooltip("GROUND OR PLANE MUST BE SET GROUMD LAYER")] [SerializeField]
    private bool isShield = true;

    [SerializeField] private GameObject[] armors;
    [SerializeField] private LayerMask mask;

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
        BossABomb.bossABigBombEvent += BossABigBombDie;
        _shieldUI.ResetShield();
    }

    void Initialize()
    {
        _currentHp.Value = maxHp;
    }

    public void OnTakeDamage(int value, Vector3 sparkleDirection, Quaternion rotation)
    {
        Debug.Log("rhino take damage");
        bt.SendEvent("OnTakeDamage");

        if (isShield)
        {
            LemonBase.onUseBTSpeak?.Invoke(LemonSpeakEnum.Shield);
            SparkleEffect.onPlaySparkleEffect(SparkleType.Shield, sparkleDirection, rotation);

            _shieldUI.HitShield(1);
            return;
        }


        onTakeDamagedEvent?.Invoke();


        AudioManager.Instance.PlaySFX("MobInjured");
        SparkleEffect.onPlaySparkleEffect(SparkleType.Normal, sparkleDirection, rotation);

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
        AutoTurnAroundDetect.onRemoveDetectList?.Invoke(gameObject);

        onDiedEvent?.Invoke();
        bt.SendEvent("OnDied");
    }

    public void OnStun()
    {
        bt.SendEvent("OnStun");
    }


    public void OnTakeShield(int removeValue)
    {
        if (!isShield) return;

        SharedBool _isShield = false;
        bt.SetVariable("isShield", _isShield);

        // bt.SendEvent("OnTakeDamage");
        Observable.EveryUpdate().First().Delay(TimeSpan.FromSeconds(.1f)).Subscribe(_ =>
        {
            foreach (var obj in armors)
            {
                obj.SetActive(false);
            }
        }).AddTo(this);

        _shieldUI.BreakShield(0);

        isShield = false;
    }

    public void BossABigBombDie()
    {
        bt.SendEvent("OnStun");
        onDiedEvent?.Invoke();

        Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(2)).First().Subscribe(_ => { Destroy(gameObject); })
            .AddTo(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((mask & (1 << other.gameObject.layer)) == 0) return;
        bt.SendEvent("OnHitWall");
    }

    private void OnDisable()
    {
        AutoTurnAroundDetect.onRemoveDetectList?.Invoke(gameObject);
        BossABomb.bossABigBombEvent -= BossABigBombDie;
    }
}
using System;
using _.Scripts.Enemy;
using _.Scripts.Event;
using _.Scripts.Interface;
using _.Scripts.Level;
using UnityEngine;
using UnityEngine.Events;

public class EliteHandBase : Enemy, IDamageable, IBreakable
{
    

    [SerializeField] private BreakState breakState;


    [SerializeField] private int hp;
    [SerializeField] private UnityEvent onTakeDamagedEvent;
    [SerializeField] private UnityEvent onDiedEvent;
    private HandEffect _hadnEffect;

    private void OnEnable()
    {
        SystemActions.onPlayerRespawn += PlayerRespawnSetting;
    }

    private void OnDisable()
    {
        SystemActions.onPlayerRespawn -= PlayerRespawnSetting;
    }

    public void PlayerRespawnSetting()
    {
        Destroy(gameObject,2);
    }

    public void OpenBT()
    {
        bt.enabled = true;
    }

    public void OnTakeDamage(int value, Vector3 sparkleDirection, Quaternion rotation)
    {
        SystemActions.onFrameSlow?.Invoke(0.03f); // 调用帧率减慢事件
        SparkleEffect.onPlaySparkleEffect(SparkleType.Normal, sparkleDirection, rotation);

        if (hp <= 0) OnDied();

        onTakeDamagedEvent?.Invoke();
        hp -= value;
    }

    public void OnDied()
    {
        onDiedEvent?.Invoke();
    }


    void Start()
    {
        _hadnEffect = GetComponent<HandEffect>();
        SwitchBreakMaterial();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<CartRhinoBase>(out var cartRhino))
        {
            if (cartRhino.isGetCatch) return;
            cartRhino.CatchRhino();
            bt.SendEvent("CatchRhino");
        }

        if (other.gameObject.TryGetComponent<EliteStoneWall>(out var wall))
        {
            wall.BreakWall();
        }
    }


    public void SwitchBreakMaterial()
    {
        if (breakState == BreakState.Break1)
            _hadnEffect.SwitchBreakMaterial(BreakState.Break1);

        if (breakState == BreakState.Break2)
            _hadnEffect.SwitchBreakMaterial(BreakState.Break2);
    }

    public void OnTakeBreakableAttack()
    {
        breakState += 1;
        SwitchBreakMaterial();
        bt.SendEvent("Runaway");
        TimeLineManager.Instance.PlayTimeLine(4);
    }
}
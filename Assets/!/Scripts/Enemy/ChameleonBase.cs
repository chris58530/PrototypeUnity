using _.Scripts.Enemy;
using _.Scripts.Event;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class ChameleonBase : Enemy, IDamageable
{
    public Image hpImage;
    [SerializeField] private float maxHp;
    private ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();
    [SerializeField] private UnityEvent onTakeDamagedEvent;
    [SerializeField] private UnityEvent onStunEvent;
    [SerializeField] private UnityEvent onDiedEvent;
    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        _currentHp.Value = maxHp;
    }

    public void OnTakeDamage(int value,Vector3 sparkleDirection,Quaternion rotation)
    {
        SystemActions.onFrameSlow?.Invoke(0.03f);  // 调用帧率减慢事件

        onTakeDamagedEvent?.Invoke();
AudioManager.Instance.PlaySFX("MobInjured");
        if (_currentHp.Value <= 0)
        {
            OnDied();
            return;
        }

        bt.SendEvent("GetHurt");
        _currentHp.Value -= value;
        if (_currentHp.Value <= 0)
        {
            bt.SendEvent("OnStun");
            onStunEvent?.Invoke();
        }
    }

    public void OnDied()
    {
        AutoTurnAroundDetect.onRemoveDetectList?.Invoke(gameObject);

        onDiedEvent?.Invoke();
        bt.SendEvent("OnDied");
    }
}
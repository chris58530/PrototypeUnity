using _.Scripts.Enemy;
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

    public void OnTakeDamage(int value)
    {
        onTakeDamagedEvent?.Invoke();

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
        onDiedEvent?.Invoke();
        bt.SendEvent("OnDied");
    }
}
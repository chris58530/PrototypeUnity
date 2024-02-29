using _.Scripts.Enemy;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
//coldwater
using System.Collections;
using UnityEngine.Events;
using UnityEngine.Serialization;


public class ChameleonBase : Enemy, IDamageable
{
    public Image hpImage;

   

    [SerializeField] private float maxHp;
    private ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();
    [SerializeField] private UnityEvent onTakeDamagedEvent;
    [SerializeField] private UnityEvent onDiedEvent;
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
        onTakeDamagedEvent?.Invoke();
        bt.SendEvent("GetHurt");
        _currentHp.Value -= value;
      
        if (_currentHp.Value <= 0) OnDied();
    }

    public void OnDied()
    {
        onDiedEvent?.Invoke();
        bt.SendEvent("OnDied");
    }

 }
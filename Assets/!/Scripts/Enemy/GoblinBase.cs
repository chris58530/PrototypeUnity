using System.Collections;
using System.Collections.Generic;
using _.Scripts.Enemy;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class GoblinBase : Enemy, IDamageable, IDashable
{
    [SerializeField] private float maxHp;
    private ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();

    [SerializeField] private Material InLightMaterial;
    [SerializeField] private Material OringinMaterial;
    [SerializeField] private Renderer[] _renderers;

    public bool canDash { get; set; }

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
        canDash = true;
    }

    public void OnTakeDamage(int value)
    {
        if (canDash)
        {
            // bt.SendEvent("DoDash");
            DoDash();
            return;
        }

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

    public void InLight(bool isLight)
    {
        if (isLight)
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material = InLightMaterial;
            }
        else
            for (var i = 0; i < _renderers.Length; i++)
            {
                var t = _renderers[i];
                t.material = OringinMaterial;
            }
    }

    void DoDash()
    {
        Transform destination = GameObject.Find("GloblinPosition").transform;
        transform.position = destination.position;
    }

    public void OnDied()
    {
        onDiedEvent?.Invoke();
    }

    public void OnStun()
    {
        bt.SendEvent("OnStun");
    }
}
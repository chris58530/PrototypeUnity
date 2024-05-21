using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VTabs.Libs;

public class StoneWall : MonoBehaviour, IDamageable
{
    [SerializeField] private StoneUI stoneUI;
    [SerializeField] private float maxHp = 3;
    [SerializeField] private UnityEvent onDiedEvent;
    private float _currentHp;

    private void Start()
    {
        _currentHp = maxHp;
    }

    public void OnTakeDamage(int value, Vector3 sparkleDirection, Quaternion rotation)
    {
        Debug.Log(_currentHp + " took damage");
        _currentHp -= 1;
        stoneUI.UpdateHpImage(_currentHp, maxHp);
        if (_currentHp <= 0)
        {
            OnDied();
        }
    }

    public void OnDied()
    {
        onDiedEvent?.Invoke();
        this.GetComponents<Collider>().ForEach(c => c.enabled = false);
    }
}
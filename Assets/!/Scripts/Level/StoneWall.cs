using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using UnityEngine;
using UnityEngine.Events;
using VTabs.Libs;

public class StoneWall : MonoBehaviour, IDamageable
{
    [SerializeField] private StoneUI stoneUI;
    [SerializeField] private float maxHp = 3;
    [SerializeField] private UnityEvent onDiedEvent;
    private float _currentHp;
    private bool _canWallDamage = true;

    private void OnEnable()
    {
        EnemyActions.setCanDamagedEnemy += SetCanDamaged;
    }

    private void OnDisable()
    {
        EnemyActions.setCanDamagedEnemy -= SetCanDamaged;
    }

    private void Start()
    {
        _currentHp = maxHp;
    }

    void SetCanDamaged(bool canDamage)
    {
        if (canDamage)
            _canWallDamage = true;
        else
            _canWallDamage = false;
    }

    public void OnTakeDamage(int value, Vector3 sparkleDirection, Quaternion rotation)
    {
        if (!_canWallDamage) return;
        EnemyActions.setCanDamagedEnemy?.Invoke(false);
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
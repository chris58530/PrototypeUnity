using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class StoneWall : MonoBehaviour, IDamageable
{
    [SerializeField] private StoneUI stoneUI;
    [SerializeField] private float maxHp = 3;
    [SerializeField] private GameObject[] shakingGameObjects;
    [SerializeField] private UnityEvent onDiedEvent;
    private float _currentHp;
    private bool _canWallDamage = true;

    private void OnEnable()
    {
        EnemyActions.setCanDamagedEnemy += SetCanDamaged;
        EnemyActions.setCanDamagedEnemy?.Invoke(false);
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
        // foreach (var stone in shakingGameObjects)
        // {
        //     Vector3 originalPosition = stone.transform.position;
        //     stone.transform.position = sparkleDirection * 0.5f + originalPosition;
        //     stone.transform.rotation = rotation;
        // }
        Debug.Log("STONE WALL TAKE DAMAGE");
        EnemyActions.setCanDamagedEnemy?.Invoke(false);
        _currentHp -= 1;
        stoneUI.UpdateHpImage(_currentHp, maxHp);
        if (_currentHp <= 0)
        {
            OnDied();
        }
    }

    public void TakeRhinoAttack()
    {
        // stoneUI.ShowImage(true);
        stoneUI.ShowImage(true);
        _currentHp -= 1;

        stoneUI.UpdateHpImage(_currentHp, maxHp);

        if (_currentHp <= 0)
        {
            OnDied();
        }
    }

    public void OnDied()
    {
        Debug.Log("die _currentHp + \" took damage\"");

        onDiedEvent?.Invoke();

        Collider[] colliders = this.GetComponents<Collider>();
        foreach (var VARIABLE in colliders)
        {
            VARIABLE.enabled = false;
        }
        Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(1f))
            .Subscribe(_ => { stoneUI.ScaleShowImage(false); }).AddTo(this);
    }
}
using System;
using _.Scripts.Interface;
using _.Scripts.Player.Props;
using UniRx;
using UnityEngine;

public class Crystal : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHp;
    private int _currentHp;
    [SerializeField] private GameObject detroyCrystalParticle;

    [Header("If can relife ")] [SerializeField]
    private bool canRelife;

    [SerializeField] private float relifeTime;
    private Collider _collider;


    private void OnEnable()
    {
        _currentHp = maxHp;
    }

    public void OnTakeDamage(int value)
    {
        Debug.Log($"{this.name + " " + "on take damage"}");
        if (_currentHp > 1)
        {
            _currentHp--;
        }
        else OnDied();
    }


    public void OnDied()
    {
        Instantiate(detroyCrystalParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
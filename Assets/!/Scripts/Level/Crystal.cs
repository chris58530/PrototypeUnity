using System;
using _.Scripts.Interface;
using _.Scripts.Player.Props;
using UniRx;
using UnityEngine;

public class Crystal : MonoBehaviour,IAbilityContainer
{
    [SerializeField] private int maxHp;
    private int _currentHp;
    [SerializeField] private bool canRelife;
    [SerializeField] private float relifeTime;
    [SerializeField] private GameObject detroyCrystal;
    private Collider _collider;


    private void OnEnable()
    {
        _currentHp = maxHp;
        Destroy(gameObject,5);

    }

    public void OnTakeDamage(int value)
    {
        Debug.Log($"{this.name+" "+"on take damage"}");
        if (_currentHp > 1)
        {
            _currentHp--;
        }
        else OnDied();
    }

  

    public void OnDied()
    {
     Destroy(gameObject);
    }

    public AbilityWeapon.AbilityType GetAbility()
    {
        OnDied();
        return AbilityWeapon.AbilityType.Strength;
    }
}
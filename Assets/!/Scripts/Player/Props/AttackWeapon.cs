using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Player;
using TMPro;
using UnityEngine;
using UniRx;
public class AttackWeapon : Weapon
{
    public int maxShieldValue;
    public int currentShieldValue;
    private IDisposable _chargeDisposable;
    private void Start()
    {
        transform.parent = GameObject.Find("SwordPoint").transform;
        transform.gameObject.SetActive(false);

    }
  
    public void Charge(float chargeTime,float currentValue)
    {
        currentShieldValue = (int)currentValue;
        _chargeDisposable = Observable.Interval(TimeSpan.FromSeconds(chargeTime))
            .Where(_ => currentShieldValue < maxShieldValue)
            .Subscribe(_ =>
            {
                currentShieldValue += 1;
                Debug.Log(currentShieldValue);

            }).AddTo(this);
    }

    public int PickUpAndGetValue()
    {
        Debug.Log(currentShieldValue + "has been input");

        _chargeDisposable?.Dispose();
        return currentShieldValue;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<IDamageable>(out var damageObj)) return;
        if (other.gameObject.layer != 7 ) return;

        damageObj.OnTakeDamage(attackValue);
        Debug.Log("攻擊了 : " + attackValue );
        //dubug
        TMP_Text t = GameObject.Find("AttackValueText").GetComponent<TMP_Text>();
        t.text = (attackValue ).ToString();
    }
}
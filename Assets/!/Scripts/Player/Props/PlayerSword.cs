using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;

public class PlayerSword : MonoBehaviour
{
    public int maxShieldValue;
    public int currentShieldValue;
    private IDisposable _chargeDisposable;

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
}
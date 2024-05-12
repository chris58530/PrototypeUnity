using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnFire : MonoBehaviour
{
    [SerializeField] private GameObject fireEffect;
    public static bool isOnFire;
    public static Action<bool> onFire;


    private void OnEnable()
    {
        SetFireEffect(false);

        onFire += SetFireEffect;
    }

    private void OnDisable()
    {
        onFire -= SetFireEffect;
    }

    public void SetFireEffect(bool on)
    {
        fireEffect.SetActive(on);
        isOnFire = on;
    }
}
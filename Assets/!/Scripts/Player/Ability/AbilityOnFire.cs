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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            AbilityOnFire.onFire?.Invoke(true);
        }
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
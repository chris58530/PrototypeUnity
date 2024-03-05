using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDoor : MonoBehaviour,IDamageable
{
    [SerializeField] private GameObject destroyObj;
    [SerializeField] private GameObject StoneBreakVFX;
    public void OnTakeDamage(int value)
    {
        AudioManager.Instance.PlaySFX("HitOnBigRock");
        Instantiate(StoneBreakVFX, transform.position, Quaternion.identity);
        OnDied();
    }

    public void OnDied()
    {
        Destroy(destroyObj);
    }
}

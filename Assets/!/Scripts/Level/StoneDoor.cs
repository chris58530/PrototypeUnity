using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDoor : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject destroyObj;
    [SerializeField] private ParticleSystem StoneBreakVFX;
    [SerializeField] private GameObject BoomStone;

    public void OnTakeDamage(int value)
    {
        // if (StoneBreakVFX != null)
        //     Instantiate(StoneBreakVFX, transform.position, Quaternion.identity);
        StoneBreakVFX.Play();
        AudioManager.Instance.PlaySFX("HitOnBigRock");
        BoomStone.SetActive(true);
        OnDied();
        
    }

    public void OnDied()
    {
        Destroy(destroyObj);
    }
}
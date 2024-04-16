using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _.Scripts.Event;

public class EatCollider : MonoBehaviour
{
    [SerializeField] private ParticleSystem crystalParticleSystem;

    private void OnEnable()
    {
        PlayerActions.endPlayerEatEffect += StopCrystalParticle;
        crystalParticleSystem.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        PlayerActions.endPlayerEatEffect -= StopCrystalParticle;
        StopCrystalParticle();
    }


    private void Start()
    {
        StopCrystalParticle();
    }

    void StopCrystalParticle()
    {
        crystalParticleSystem.Stop();
        crystalParticleSystem.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<Crystal>()) return;
        crystalParticleSystem.gameObject.SetActive(true);

        crystalParticleSystem.Play();
    }
}
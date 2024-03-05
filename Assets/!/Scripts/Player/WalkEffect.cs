using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using UnityEngine;

public class WalkEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem walkParticle;

    private void OnEnable()
    {
        PlayerActions.onPlayerWalk += Walking;
    }

    private void OnDisable()
    {
        PlayerActions.onPlayerWalk -= Walking;
    }

    void Walking(bool isWalk)
    {
        if (isWalk)
            walkParticle.Play();
        else 
            walkParticle.Stop();

    }
 
}
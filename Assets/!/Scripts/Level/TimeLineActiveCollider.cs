using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineActiveCollider : MonoBehaviour
{
    [SerializeField] private PlayableDirector introTimeLineManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            introTimeLineManager.Play();
            Destroy(gameObject);
        }
    }
}
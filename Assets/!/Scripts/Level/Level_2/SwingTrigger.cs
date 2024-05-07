using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Playables;

public class SwingTrigger : MonoBehaviour
{
    [SerializeField] private GameObject swingRhino;
    [SerializeField] private Transform rhinoSpawnPoint;

    [SerializeField] private float resetTime;
    [SerializeField] private PlayableDirector down;
    [SerializeField] private PlayableDirector up;
    
    public static Action OnSwingTrigger;

    private void OnEnable()
    {
        OnSwingTrigger += DownTimeline;
    }

    private void OnDisable()
    {
        OnSwingTrigger -= DownTimeline;

    }

    public void DownTimeline()
    {
        down.Play();
        Instantiate(swingRhino, rhinoSpawnPoint.position, rhinoSpawnPoint.rotation);
    }
    public void UpTimeline()
    {
        up.Play();
 
    }
}
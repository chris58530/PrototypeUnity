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
    public void DownTimeline()
    {
        down.Play();
    }
    public void UpTimeline()
    {
        up.Play();
        GetComponent<Collider>().enabled = false;
        Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(resetTime)).First().Subscribe(_ =>
        {
            GetComponent<Collider>().enabled = true;
            DownTimeline();
            Instantiate(swingRhino, rhinoSpawnPoint.position, rhinoSpawnPoint.rotation);
        }).AddTo(this);
    }
}
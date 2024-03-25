using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using Unity.VisualScripting;

public class TimeLineManager : _.Scripts.Tools.Singleton<TimeLineManager>
{
    [SerializeField] private PlayableDirector[] playableDirectors;
    [HideInInspector] public PlayableDirector currentDirector;


    public static Action onPlayTimelLine;
    public static Action onQuitTimelLine;

    private void Update()
    {
        if (currentDirector == null) return;

        SpeedUpDirectors();
        QuitTimeLineDetect();
    }

    public void PlayTimeLine(int num)
    {
        onPlayTimelLine?.Invoke();
        currentDirector = playableDirectors[num];
        currentDirector.Play();
    }

    private void QuitTimeLineDetect()
    {
        if (currentDirector.time == 0)
        {
            onQuitTimelLine?.Invoke();
        }
    }

    private void SpeedUpDirectors()
    {
        if (currentDirector.duration - 2 <= currentDirector.time) return;

        if (Input.GetKey(KeyCode.Q))
        {
            currentDirector.time += 0.05f;
        }
    }
}
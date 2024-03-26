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

    private bool _isExecuteQuitAction;

    private void Update()
    {
        if (currentDirector == null) return;
        if (_isExecuteQuitAction) return;

        SpeedUpDirectors();
    }

    public void PlayTimeLine(int num)
    {
        onPlayTimelLine?.Invoke();
        currentDirector = playableDirectors[num];
        currentDirector.Play();
        _isExecuteQuitAction = false;
        currentDirector.stopped += QuitTimeLineDetect;
    }

    private void QuitTimeLineDetect(PlayableDirector playableDirector)
    {
        if (currentDirector.duration <= currentDirector.time && _isExecuteQuitAction) return;
        Debug.Log("QuitTimeLineDetect");
        onQuitTimelLine?.Invoke();
        _isExecuteQuitAction = true;
        currentDirector = null;
    }

    private void SpeedUpDirectors()
    {
        if (currentDirector.duration - 2 <= currentDirector.time) return;

        if (Input.GetKey(KeyCode.Q))
        {
            currentDirector.time += 0.1f;
        }
    }

    private void OnEnable()
    {
       
    }
}
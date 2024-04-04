using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using Unity.VisualScripting;
using UnityEngine.Timeline;

public class TimeLineManager : _.Scripts.Tools.Singleton<TimeLineManager>
{
    [SerializeField] private PlayableDirector[] playableDirectors;
    [HideInInspector] public PlayableDirector currentDirector;


    public static Action onPlayTimelLine;
    public static Action onQuitTimelLine;

    private bool _isExecuteQuitAction;
    [SerializeField] private int currentActiveDirectorNumber = 0;
    private bool _isPauseTimeLine;

    private void Update()
    {
        if (currentDirector == null) return;
        ContinueTimeline();
        SpeedUpDirectors();
    }

    public void PlayTimeLine(int num)
    {
        //Insure current time line is follow the flow
        // if (num != currentActiveDirectorNumber) return;
        currentActiveDirectorNumber++;
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
        if (_isPauseTimeLine) return;
        if (_isExecuteQuitAction) return;

        if (currentDirector.duration - 1.5f <= currentDirector.time) return;

        if (Input.GetKey(KeyCode.Q))
        {
            currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(10f);
        }
        else currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(1f);
    }

    void ContinueTimeline()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(1f);
            // currentDirector.Resume();
            _isPauseTimeLine = false;
            Debug.Log("time line continue");
        }
    }

    public void StopTimeLine()
    {
        if (currentDirector == null) return;
        _isPauseTimeLine = true;
        // currentDirector.Pause();
        Debug.Log("Pause");

        currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(0f);

        var timelineAsset = currentDirector.playableAsset as TimelineAsset;


        Debug.Log(timelineAsset.markerTrack.GetMarkers());
    }


    private void OnEnable()
    {
    }
}
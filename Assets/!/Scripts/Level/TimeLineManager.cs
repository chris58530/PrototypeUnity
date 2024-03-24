using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using _.Scripts.Tools;

public class TimeLineManager : Singleton<TimeLineManager>
{
    [SerializeField] private PlayableDirector[] playableDirectors;
    private PlayableDirector _currentDirector;
    private void OnEnable()
    {
        
    }

    public void PlayTimeLine(int num)
    {
        _currentDirector = playableDirectors[num];
        _currentDirector.Play();
    }
}
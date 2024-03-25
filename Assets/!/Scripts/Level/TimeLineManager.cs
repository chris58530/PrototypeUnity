using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using _.Scripts.Tools;

public class TimeLineManager : Singleton<TimeLineManager>
{
    [SerializeField] private PlayableDirector[] playableDirectors;
    private PlayableDirector _currentDirector;

    private void Update()
    {
        if (_currentDirector == null) return;
        if (_currentDirector.duration - 2 <= _currentDirector.time) return;
        
        if (Input.GetKey(KeyCode.Q))
        {
            _currentDirector.time += 0.05f;
        }
    }

    public void PlayTimeLine(int num)
    {
        _currentDirector = playableDirectors[num];
        _currentDirector.Play();
    }
}
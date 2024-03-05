using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Tools;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineController : Singleton<TimeLineController>
{
    [SerializeField] private PlayableDirector introDirector;
    [SerializeField] private PlayableDirector endDirector;
    [SerializeField] private BehaviorTree quackTree;
    private PlayableDirector _currentDirector;
    private void Update()
    {
        if (_currentDirector.duration - 2 <= _currentDirector.time)
        {
            SetCanFight();
            return;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            _currentDirector.time += 0.05f;
        }
    }

    public void PlayTimeLine(int num)
    {
        switch (num)
        {
            case 1:
                _currentDirector = introDirector;
                _currentDirector.Play();
                break;
            case 2:
                _currentDirector = endDirector;
                _currentDirector.Play();
                break;
        }
    }
    public void SetCanFight()
    {
        SharedBool isFighting = true;
        quackTree.SetVariable("isFighting", isFighting);
    }
}
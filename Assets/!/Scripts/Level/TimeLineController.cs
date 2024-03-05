using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineController : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private BehaviorTree quackTree;

    private void Update()
    {
        if (_director.duration - 5 <= _director.time)
        {
            SetCanFight();
            return;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            _director.time += 0.05f;
        }
    }

    public void SetCanFight()
    {
        SharedBool isFighting = true;
        quackTree.SetVariable("isFighting", isFighting);
    }
}
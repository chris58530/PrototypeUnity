using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bell : MonoBehaviour
{
    [Tooltip("TimeLineManagers PlayableDirector Number")] [SerializeField]
    private int timeLineNumber;

    private Animator ani;

    private bool _isPlayed;

    private void Start()
    {
        ani = GetComponentInChildren<Animator>();
    }

    public void PlayAnimation()
    {
        if (!_isPlayed)
        {
            _isPlayed = true;
            TimeLineManager.Instance.PlayTimeLine(timeLineNumber);

        }

        if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            ani.Play("Ring");
            ani.CrossFade("Ring", 0.1f);
        }
    }
}
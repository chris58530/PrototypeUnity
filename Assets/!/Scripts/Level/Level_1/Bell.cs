using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Level;
using UnityEngine;


public class Bell : MonoBehaviour, ITaskResult
{
    [Tooltip("TimeLineManagers PlayableDirector Number")] [SerializeField]
    private int timeLineNumber;

    private Animator ani;


    [SerializeField]private bool canPlay;

    private void Start()
    {
        ani = GetComponentInChildren<Animator>();
    }

    public void PlayAnimation()
    {
        if (canPlay)
        {
            TimeLineManager.Instance.PlayTimeLine(timeLineNumber);
            Debug.Log("PlayTimeLine");

            canPlay = false;
        }

        if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            ani.Play("Ring");
            ani.CrossFade("Ring", 0.1f);
            AudioManager.Instance.PlaySFX("BellCalling");
        }
    }

    public void DoResult()
    {
        canPlay = true;
    }

}

public class ObjectSoundEffect : MonoBehaviour
{
    
}
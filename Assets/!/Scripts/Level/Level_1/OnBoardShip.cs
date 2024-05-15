using System.Collections;
using System.Collections.Generic;
using _.Scripts.Level;
using UnityEngine;

public class OnBoardShip : MonoBehaviour
{
    [Tooltip("TimeLineManagers PlayableDirector Number")] [SerializeField]
    private int timeLineNumber;

    void Start()
    {
        TimeLineManager.Instance.PlayTimeLine(timeLineNumber);
    }

  
}
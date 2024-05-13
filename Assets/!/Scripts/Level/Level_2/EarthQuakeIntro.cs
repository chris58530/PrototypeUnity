using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuakeIntro : MonoBehaviour
{
    [Tooltip("TimeLineManagers PlayableDirector Number")] [SerializeField]
    private int timeLineNumber;

    void Start()
    {
        TimeLineManager.Instance.PlayTimeLine(timeLineNumber);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAEndPerformance : MonoBehaviour
{
    void Start()
    {
        TimeLineController.Instance.PlayTimeLine(2);

    }
}
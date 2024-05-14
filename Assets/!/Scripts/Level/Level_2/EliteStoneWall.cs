using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using UnityEngine;
using UnityEngine.Playables;

public class EliteStoneWall : MonoBehaviour
{
    [SerializeField] GameObject breakStoneWallTimeline;

    public void BreakWall()
    {
        SystemActions.onCameraShake.Invoke();
        breakStoneWallTimeline.SetActive(true);
        Destroy(gameObject);
    }
}
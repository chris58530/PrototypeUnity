using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Level;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class BossIntroTimelineTriggerObject : MonoBehaviour
{
    [SerializeField] private PlayableDirector earthquakeTimeline;
    [SerializeField] private int timelineNumber;


    public void TriggerTimeline()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //check Coroutine is not already playing

            StartCoroutine(IntroTimeline());
            GetComponent<Collider>().enabled = false;
        }
    }

    IEnumerator IntroTimeline()
    {
        earthquakeTimeline.Play();
        yield return new WaitForSeconds(5);
        TimeLineManager.Instance.PlayTimeLine(timelineNumber);
    }
}
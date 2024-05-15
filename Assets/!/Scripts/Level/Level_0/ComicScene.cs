using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Level;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TimeLineManager.onQuitTimelLine += ChangeScene;
        Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(2)).First().Subscribe(_ =>
        {
            TimeLineManager.Instance.PlayTimeLine(0);

        }).AddTo(this);
    }

    private void OnDisable()
    {
        TimeLineManager.onQuitTimelLine -= ChangeScene;

    }

    void ChangeScene()
    {
        SceneManager.LoadScene("Level 1");
    }
}
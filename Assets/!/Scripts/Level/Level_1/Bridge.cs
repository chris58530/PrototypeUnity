using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Level;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public class Bridge : MonoBehaviour
{
    [SerializeField] private GameObject brige;
        [SerializeField] private GameObject puller;

    [SerializeField] private TaskObject[] taskObj;
    [SerializeField] private bool isPull = false;

    private void Update()
    {
        //改用事件呼叫
        CheckTask();
    }

    void CheckTask()
    {
        int checkCount = 0;
        foreach (var task in taskObj)
        {
            if (task.isDone) checkCount++;
        }

        if (checkCount >= taskObj.Length)
        {
             if (!isPull)
        {
            Observable.EveryUpdate().First().Subscribe(_ =>
            {
                if (!isPull)
                {
                puller.GetComponent<Animator>().Play("PullDown");
                AudioManager.Instance.PlaySFX("PullingController");
                isPull = true; // 設置標誌，表示已經播放過 PullingController 音效
                }

                Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(__ =>
                {
                    brige.GetComponent<Animator>().Play("PutDownBridge");
                    AudioManager.Instance.PlaySFX("OpenBridge");
                    Debug.Log("ppppppppppppppppppppppp");
                    Destroy(gameObject);
                }).AddTo(this);

            }).AddTo(this);
        }
            // Observable.EveryUpdate().First().Subscribe(_ =>
            // {
            //     brige.GetComponent<Animator>().Play("PutDownBridge");
            //     AudioManager.Instance.PlaySFX("PullingController");

            //     AudioManager.Instance.PlaySFX("OpenBridge");
            //     Debug.Log("ppppppppppppppppppppppp");
            //     Destroy(gameObject);
            // }).AddTo(this);
        }
    }
}
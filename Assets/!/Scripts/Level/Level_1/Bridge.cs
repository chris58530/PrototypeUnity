using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Level;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public class Bridge : MonoBehaviour, ITaskResult
{
    [SerializeField] private GameObject brige;
    [SerializeField] private GameObject puller;

    private bool isPull;


    public void DoResult()
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

                Observable.Timer(TimeSpan.FromSeconds(2)).First().Subscribe(__ =>
                {
                    brige.GetComponent<Animator>().Play("PutDownBridge");
                    AudioManager.Instance.PlaySFX("OpenBridge");
                    Destroy(gameObject);
                }).AddTo(this);
            }).AddTo(this);
        }

        Observable.EveryUpdate().First().Subscribe(_ =>
        {
            brige.GetComponent<Animator>().Play("PutDownBridge");
            AudioManager.Instance.PlaySFX("PullingController");

            AudioManager.Instance.PlaySFX("OpenBridge");
            Debug.Log("ppppppppppppppppppppppp");
            Destroy(gameObject);
        }).AddTo(this);
    }
}
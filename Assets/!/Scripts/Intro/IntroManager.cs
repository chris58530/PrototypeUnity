using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TimeSpan = MagicaCloth2.TimeSpan;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private GameObject destroyObject;
    public PlayableDirector playableDirector;
    private void Start()
    {
        int damage = 0;
        destroyObject.OnTriggerEnterAsObservable().Subscribe(collider1 =>
        {
            if (collider1.gameObject.CompareTag("Sword"))
            {
                damage++;
                Debug.Log(damage);
                if (damage >= 1)
                    playableDirector.Play();
            }
        }).AddTo(this);
destroyObject.OnDisableAsObservable().Subscribe(_ => { SwitchScene(); }).AddTo(this);
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene("Level 1");
    }
}
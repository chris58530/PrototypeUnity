using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingControl : MonoBehaviour
{
    [SerializeField] private float timeToMenu;

    private void Start()
    {
        Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(timeToMenu)).Subscribe(_ =>
        {
            SceneManager.LoadScene("Menu");
        }).AddTo(this);
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Menu");

        }
    }
}
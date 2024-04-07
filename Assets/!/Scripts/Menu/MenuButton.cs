using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private GameObject introIcon;

    private void Start()
    {
        Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(3)).First().Subscribe(_ =>
        {
            introIcon.SetActive(false);
        }).AddTo(this);
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("Comic");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
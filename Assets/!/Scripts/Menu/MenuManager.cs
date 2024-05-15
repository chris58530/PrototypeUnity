using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts;
using _.Scripts.Level;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private IDisposable _logoDisposable;


    public void OnClickStart()
    {
        TimeLineManager.Instance.PlayTimeLine(0);
        GameManager.Instance.SwitchScene(1,2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
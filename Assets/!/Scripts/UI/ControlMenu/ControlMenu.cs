using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum ControlMenuState
{
    Invisible,
    Visible,
    State1,
}

public class ControlMenu : MonoBehaviour
{
    public ControlMenuState controlMenuState { get; private set; }

    
    [SerializeField] private GameObject context;

    [SerializeField] private Button continueButton;
    [SerializeField] private Button openBasicContorlButton;
    [SerializeField] private Button openAudioControlButton;

    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject audioPanelFirstSelect;
    [SerializeField] private GameObject basicContorlPanel;
    [SerializeField] private GameObject buttonGroup;

    private GameObject _lastSelectButtonObject;
    private UIInput _uiInput;

    private void Start()
    {
        _uiInput = FindObjectOfType<UIInput>();
        // _eventSystem = FindObjectOfType<EventSystem>();
    }

    void OnEnable()
    {
        openBasicContorlButton.onClick.AsObservable().Subscribe(_ =>
        {
            ChangeState(ControlMenuState.State1);
            audioPanel.SetActive(false);
            basicContorlPanel.SetActive(true);

            buttonGroup.SetActive(false);

            _lastSelectButtonObject = openBasicContorlButton.gameObject;
        }).AddTo(this);


        openAudioControlButton.onClick.AsObservable().Subscribe(_ =>
        {
            ChangeState(ControlMenuState.State1);
            audioPanel.SetActive(true);
            basicContorlPanel.SetActive(false);
            buttonGroup.SetActive(false);
            
            EventSystem.current.SetSelectedGameObject(audioPanelFirstSelect);
            _lastSelectButtonObject = openAudioControlButton.gameObject;

        }).AddTo(this);


        // Observable.EveryUpdate().Where(_ => controlMenuState == ControlMenuState.Visible).Subscribe(_ =>
        // {
        //    
        // }).AddTo(this);
        //
        // Observable.EveryUpdate().Where(_ => controlMenuState == ControlMenuState.State1).Subscribe(_ =>
        // {
        //  
        // }).AddTo(this);
    }

    public void ShowContext(bool show)
    {
        if (show)
        {
            GameManager.Instance.TimeScale(0);
            GameManager.Instance.LockPlayerInput(false);
            ChangeState(ControlMenuState.Visible);
        }
        else
        {
            GameManager.Instance.TimeScale(1);
            GameManager.Instance.LockPlayerInput(true);

            ChangeState(ControlMenuState.Invisible);
        }

        context.SetActive(show);
    }

    public void OpenControlMenu(ControlMenuState state)
    {
        switch (state)
        {
            case ControlMenuState.Invisible:
                ChangeState(ControlMenuState.Visible);
                ShowContext(true);
                buttonGroup.SetActive(true);
                audioPanel.SetActive(false);
                basicContorlPanel.SetActive(false);
                EventSystem.current.SetSelectedGameObject(continueButton.gameObject);


                break;
            case ControlMenuState.Visible:
                ChangeState(ControlMenuState.Invisible);

                ShowContext(false);
                break;

            case ControlMenuState.State1:
                ChangeState(ControlMenuState.Visible);
                audioPanel.SetActive(false);
                basicContorlPanel.SetActive(false);
                buttonGroup.SetActive(true);
                
                EventSystem.current.SetSelectedGameObject(_lastSelectButtonObject.gameObject);

                break;
        }
    }


    public void ChangeState(ControlMenuState state)
    {
        controlMenuState = state;
    }
}
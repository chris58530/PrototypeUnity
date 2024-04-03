using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum ControlMenuState
{
    Invisible,
    Visible,
    State1,
    State2,
}

public class ControlMenu : MonoBehaviour
{
    [SerializeField] private GameObject context;
    [SerializeField] private Button openBasicContorlButton;
    [SerializeField] private Button closeBasicContorlButton;

    [SerializeField] private Button openAudioControlButton;
    [SerializeField] private Button closeAudioControlButton;

    public ControlMenuState controlMenuState { get; private set; }
    private UIInput _uiInput;

    private void Start()
    {
        _uiInput = FindObjectOfType<UIInput>();
    }

    void OnEnable()
    {
        openBasicContorlButton.onClick.AsObservable().Subscribe(_ =>
        {
            ChangeState(ControlMenuState.State1);

            GetComponentInChildren<BasicContorlButton>().ShowControlImage(true);
        }).AddTo(this);
        closeBasicContorlButton.onClick.AsObservable().Subscribe(_ => { ChangeState(ControlMenuState.Visible); })
            .AddTo(this);

        openAudioControlButton.onClick.AsObservable().Subscribe(_ =>
        {
            ChangeState(ControlMenuState.State1);

            GetComponentInChildren<AudioControlButton>().ShowAudioControlPanel(true);
        }).AddTo(this);
        closeAudioControlButton.onClick.AsObservable().Subscribe(_ => { ChangeState(ControlMenuState.Visible); })
            .AddTo(this);

        Observable.EveryUpdate().Where(_ => controlMenuState == ControlMenuState.Visible).Subscribe(_ =>
        {
            Debug.Log("Visible");

            GetComponentInChildren<BasicContorlButton>().ShowControlImage(false);
            GetComponentInChildren<AudioControlButton>().ShowAudioControlPanel(false);
        }).AddTo(this);

        Observable.EveryUpdate().Where(_ => controlMenuState == ControlMenuState.State1).Subscribe(_ =>
        {
            Debug.Log("State1");
        }).AddTo(this);
    }

    public void ShowContext(bool show)
    {
        if (show)
        {
            GameManager.Instance.TimeScale(0);

            ChangeState(ControlMenuState.Visible);
        }
        else
        {
            GameManager.Instance.TimeScale(1);
          
            ChangeState(ControlMenuState.Invisible);
        }

        context.SetActive(show);
    }

    private void Update()
    {
        // Debug.Log(controlMenuState);
    }

    private void OnDisable()
    {
    }

    public void ChangeState(ControlMenuState state)
    {
        controlMenuState = state;
    }
}
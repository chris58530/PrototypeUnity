using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _.Scripts.Tools;
using UnityEngine.Serialization;

public class UIManager : Singleton<UIManager>
{
    private UIInput _uiInput;

    [SerializeField] private ControlMenu controlMenu;

    private void Start()
    {
        _uiInput = GetComponent<UIInput>();
    }

    private void Update()
    {
        if (_uiInput.OpenUI)
        {
            OpenControlMenu(controlMenu.controlMenuState);
        }
    }

    void OpenControlMenu(ControlMenuState state)
    {
        switch (state)
        {
            case ControlMenuState.Invisible:

                controlMenu.ShowContext(true);
                controlMenu.ChangeState(ControlMenuState.Visible);


                break;
            case ControlMenuState.Visible:
                controlMenu.ChangeState(ControlMenuState.Invisible);

                controlMenu.ShowContext(false);
                break;

            case ControlMenuState.State1:
                controlMenu.ChangeState(ControlMenuState.Visible);
                break;
        }
    }
}
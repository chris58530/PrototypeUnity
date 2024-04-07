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
            controlMenu.OpenControlMenu(controlMenu.controlMenuState);
        }
    }

    
}
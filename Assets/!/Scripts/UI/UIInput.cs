using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInput : MonoBehaviour
{
    private PlayerCustomInput _input;

    public bool OpenUI => _input.UI.OpenClose.WasPressedThisFrame();
    public bool Confirm => _input.UI.Confirm.WasPressedThisFrame();

    public Vector2 Select => _input.UI.Control.ReadValue<Vector2>();

    public bool Selecting => Select.x != 0 || Select.y != 0;

   

    private void Awake()
    {
        _input = new PlayerCustomInput();
        OpenInput();
    }

    // private void OnEnable()
    // {
    //     TimeLineManager.onPlayTimelLine += CloseInput;
    //     TimeLineManager.onQuitTimelLine += OpenInput;
    // }
    //
    //
    // private void OnDisable()
    // {
    //     TimeLineManager.onPlayTimelLine -= CloseInput;
    //     TimeLineManager.onQuitTimelLine -= OpenInput;
    //
    //     CloseInput();
    // }

    private void OpenInput()
    {
        _input.Enable();
    }

    private void CloseInput()
    {
        _input.Disable();
    }
}
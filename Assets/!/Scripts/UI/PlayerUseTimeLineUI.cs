using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerUseTimeLineUI : MonoBehaviour
{
    [SerializeField] private GameObject keyboardConfirmImage;
    [SerializeField] private GameObject gamepadConfirmImage;

    private void OnEnable()
    {
        keyboardConfirmImage.SetActive(false);
        gamepadConfirmImage.SetActive(false);
    }

    public void ShowCanConfirmImage(bool canConfirm)
    {
        gamepadConfirmImage.SetActive(false);
        keyboardConfirmImage.SetActive(false);

        if (Gamepad.current != null)
            gamepadConfirmImage.SetActive(canConfirm);
        else
            keyboardConfirmImage.SetActive(canConfirm);
    }
}
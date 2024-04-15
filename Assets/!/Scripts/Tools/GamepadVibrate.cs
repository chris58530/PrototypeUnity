using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadVibrate : MonoBehaviour
{
    private void OnEnable()
    {
        SystemActions.onGamePadVibrate += VibrateGamepad;
    }

    private void OnDisable()
    {
        SystemActions.onGamePadVibrate -= VibrateGamepad;
    }


    void VibrateGamepad(float low, float high, float time)
    {
        Debug.Log($"--- Gamepad Vibrating {low} , {high} ---");
        StartCoroutine(VibratingGamePad(low, high, time));
    }

    IEnumerator VibratingGamePad(float low, float high, float time)
    {
        if (Gamepad.current == null) yield break;

        Gamepad.current.SetMotorSpeeds(low, high);
        Gamepad.current.ResumeHaptics();
        var endTime = Time.time + time;

        while (Time.time < endTime)
        {
            Gamepad.current.ResumeHaptics();
            yield return null;
        }

        if (Gamepad.current == null) yield break;
        Gamepad.current.PauseHaptics();
    }
}
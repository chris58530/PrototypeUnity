using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera[] virtualCameras;
    private CinemachineBasicMultiChannelPerlin _perlinNoise;

    private void Awake()
    {
        // _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        _perlinNoise = mainVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        SwitchToMainVirtualCamera();
    }

    void ShakeCamera_Hurt()
    {
        StartCoroutine(TogglePerlinNoiseAmplitude(5, 0.2f));
    }

    void ShakeCamera_HitEnemy()
    {
        StartCoroutine(TogglePerlinNoiseAmplitude(1.5f, 0.1f));
    }

    private void SwitchToMainVirtualCamera()
    {
        virtualCameras = GameObject.FindObjectsOfType<CinemachineVirtualCamera>();
        foreach (var cam in virtualCameras)
        {
            cam.gameObject.SetActive(false);
        }

        mainVirtualCamera.gameObject.SetActive(true);
    }

    private IEnumerator TogglePerlinNoiseAmplitude(float amplitude, float sceonds)
    {
        _perlinNoise.m_AmplitudeGain = amplitude;
        yield return new WaitForSeconds(sceonds);
        _perlinNoise.m_AmplitudeGain = 0;
    }

    private void OnEnable()
    {
        PlayerActions.onPlayerHurt += ShakeCamera_Hurt;
        PlayerActions.onHitEnemy += ShakeCamera_HitEnemy;
        TimeLineManager.onQuitTimelLine += SwitchToMainVirtualCamera;
    }

    private void OnDisable()
    {
        _perlinNoise.m_AmplitudeGain = 0;
        PlayerActions.onPlayerHurt -= ShakeCamera_Hurt;
        PlayerActions.onHitEnemy -= ShakeCamera_HitEnemy;
        TimeLineManager.onQuitTimelLine -= SwitchToMainVirtualCamera;
    }
}
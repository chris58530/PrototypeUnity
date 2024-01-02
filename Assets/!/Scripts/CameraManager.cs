using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Event;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
  
    [SerializeField]private CinemachineVirtualCamera _virtualCamera;
    [SerializeField]    private CinemachineBasicMultiChannelPerlin _perlinNoise;

    private void Awake()
    {
        // _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        _perlinNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void ShakeCamera()
    {
        StartCoroutine(nameof(TogglePerlinNoiseAmplitude));
    }

    private IEnumerator TogglePerlinNoiseAmplitude()
    {
        float amplitude = 5;
        _perlinNoise.m_AmplitudeGain = amplitude;
        yield return new WaitForSeconds(0.2f);
        _perlinNoise.m_AmplitudeGain = 0;
    }

    private void OnEnable()
    {
        PlayerActions.onPlayerHurt += ShakeCamera;
    }
    private void OnDisable()
    {
        PlayerActions.onPlayerHurt -= ShakeCamera;
    }
}
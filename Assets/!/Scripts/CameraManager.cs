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

    void ShakeCamera_Hurt()
    {
        StartCoroutine(TogglePerlinNoiseAmplitude(5,0.2f));
    }
    void ShakeCamera_HitEnemy()
    {
        StartCoroutine(TogglePerlinNoiseAmplitude(1.5f,0.1f));
    }

    private IEnumerator TogglePerlinNoiseAmplitude(float amplitude,float sceonds)
    {
        _perlinNoise.m_AmplitudeGain = amplitude;
        yield return new WaitForSeconds(sceonds);
        _perlinNoise.m_AmplitudeGain = 0;
    }

    private void OnEnable()
    {
        PlayerActions.onPlayerHurt += ShakeCamera_Hurt;
        PlayerActions.onHitEnemy += ShakeCamera_HitEnemy;
    }
    private void OnDisable()
    {
        PlayerActions.onPlayerHurt -= ShakeCamera_Hurt;
        PlayerActions.onHitEnemy -= ShakeCamera_HitEnemy;
    }
}
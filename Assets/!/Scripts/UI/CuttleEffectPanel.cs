using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttleEffectPanel : MonoBehaviour
{
    private float _keepTime;
    [SerializeField] private GameObject panel;

    private void LateUpdate()
    {
        if (_keepTime > 0)
        {
            _keepTime -= Time.deltaTime;
            if (!panel.activeSelf)
                panel.SetActive(true);
            return;
        }

        if (panel.activeSelf)
            panel.SetActive(false);
    }

    public void OnCuttleBulletHiiPlayer()
    {
        _keepTime = 3;
    
    }

    private void OnEnable()
    {
        _keepTime = 0;
    }

    private void OnDisable()
    {
        _keepTime = 0;
    }
}
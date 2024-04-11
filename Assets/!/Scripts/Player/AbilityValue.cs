using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityValue : MonoBehaviour
{
    [SerializeField] private Image valueImage;
    [SerializeField] private GameObject valueBG;
    private float _value;

    private void Update()
    {
        if (_value < 0) return;
        UpdateTime();
    }

    public void UpdateTime()
    {
        if(!valueBG.activeSelf)
            valueBG.SetActive(true);
        
        _value -= Time.deltaTime;
        Debug.Log($"current ability {_value} time");
        valueImage.fillAmount = _value / 1;
    }

    void StopUpdateTime()
    {
        valueBG.SetActive(false);

    }
  
}
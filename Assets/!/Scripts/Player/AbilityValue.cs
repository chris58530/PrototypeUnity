using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityValue : MonoBehaviour
{
    [SerializeField] private Image valueImage;
    [SerializeField] private GameObject valueBG;


    public void UpdateTimeImage(float value, float max)
    {
        valueBG.SetActive(true);

        valueImage.fillAmount = value / max;
    }

    public void StopUpdateTime()
    {
        valueBG.SetActive(false);
    }
}
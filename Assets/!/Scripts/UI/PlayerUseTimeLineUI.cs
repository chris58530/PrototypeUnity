using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUseTimeLineUI : MonoBehaviour
{
    [SerializeField] private GameObject confirmImage;

    private void OnEnable()
    {
        confirmImage.SetActive(false);
    }

    public void ShowCanConfirmImage(bool canConfirm)
    {
        confirmImage.SetActive(canConfirm);
    }
}

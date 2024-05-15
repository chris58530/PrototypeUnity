using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Level;
using _.Scripts.Tools;
using BehaviorDesigner.Runtime;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Playables;

public class DialogTextController : Singleton<DialogTextController>
{
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private GameObject textBackGround;

    public void TextLineOnUpdate(string line, float progress)
    {
        if (textBackGround != null)
            textBackGround.SetActive(true);
        dialogText.text = line;
        float x = dialogText.preferredWidth * progress;
        dialogText.rectTransform.sizeDelta = new Vector2(x, dialogText.rectTransform.sizeDelta.y);
    }

    private void OnQuitTimelLine()
    {
        if (textBackGround != null)
            textBackGround.SetActive(false);
        dialogText.text = "";
    }

    private void OnEnable()
    {
        TimeLineManager.onQuitTimelLine += OnQuitTimelLine;
    }

    private void OnDisable()
    {
        TimeLineManager.onQuitTimelLine -= OnQuitTimelLine;
    }
}
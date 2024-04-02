using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class AudioControlButton : MonoBehaviour
{
    [SerializeField] private GameObject audioPanel;

    public void ShowAudioControlPanel(bool open)
    {
        audioPanel.SetActive(open);
    }
}
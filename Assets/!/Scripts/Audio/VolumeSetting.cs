using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("bgmVolume"))
        {
            LoadVolume();
        }
        else
        {
            Debug.Log("no playerpref");
            SetBGMVolume();
            SetEFFVolume();
        }
    }

    public void SetBGMVolume()
    {
        float volume = bgmSlider.value;
        _mixer.SetFloat("BGM_Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("bgmVolume", volume);
    }

    public void SetEFFVolume()
    {
        float volume = effSlider.value;
        _mixer.SetFloat("EFF_Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("effVolume", volume);
    }

    public void LoadVolume()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        effSlider.value = PlayerPrefs.GetFloat("effVolume");
        SetBGMVolume();
        SetEFFVolume();
    }
}
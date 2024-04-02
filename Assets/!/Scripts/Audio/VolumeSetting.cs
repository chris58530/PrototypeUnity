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
      if (PlayerPrefs.HasKey("bgmVulome"))
      {
         LoadVolume();
      }
      else SetBGMVolume();
   }

   public void SetBGMVolume()
   {
      float volume = bgmSlider.value;
      _mixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
      PlayerPrefs.SetFloat("bgmVolume",volume);
   }

   public void LoadVolume()
   {
      bgmSlider.value = PlayerPrefs.GetFloat("BGM");
      SetBGMVolume();
   }
}

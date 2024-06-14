using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer myMixer;
    public Slider MusicSlider;

    public void SetMusicVolume()
    {
        float volume = MusicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);
    }
    public void SetButtonsVolume()
    {
        float volume = MusicSlider.value;
        myMixer.SetFloat("buttons", Mathf.Log10(volume)*20);
    }
    
}

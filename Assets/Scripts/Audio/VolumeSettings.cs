using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider musicSlider;

    void Start()
    {

        volumeSlider.value = AudioProperties.MasterVolume;
        musicSlider.value = AudioProperties.MusicVolume;

        SetMasterVolume();
        SetMusicVolume();

    }

    public void SetMasterVolume()
    {
        float volume = volumeSlider.value;
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        AudioProperties.MasterVolume = volume;

    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        AudioProperties.MusicVolume = volume;

    }
}

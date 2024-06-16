using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider VoicesVolumeSlider;
    [SerializeField] private Slider soundFXVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    private const string VoicesVolumeKey = "VoicesVolume";
    private const string SoundFXVolumeKey = "SoundFXVolume";
    private const string MusicVolumeKey = "MusicVolume";

    void Start()
    {
        LoadVolumeSettings();
    }

    public void SetVoicesVolume(float level)
    {
        audioMixer.SetFloat("VoicesVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat(VoicesVolumeKey, level);
        PlayerPrefs.Save();
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat(SoundFXVolumeKey, level);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat(MusicVolumeKey, level);
        PlayerPrefs.Save();
    }

    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey(VoicesVolumeKey))
        {
            float VoicesVolume = PlayerPrefs.GetFloat(VoicesVolumeKey);
            audioMixer.SetFloat("VoicesVolume", Mathf.Log10(VoicesVolume) * 20f);
            VoicesVolumeSlider.value = VoicesVolume;
        }

        if (PlayerPrefs.HasKey(SoundFXVolumeKey))
        {
            float soundFXVolume = PlayerPrefs.GetFloat(SoundFXVolumeKey);
            audioMixer.SetFloat("soundFXVolume", Mathf.Log10(soundFXVolume) * 20f);
            soundFXVolumeSlider.value = soundFXVolume;
        }

        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey);
            audioMixer.SetFloat("musicVolume", Mathf.Log10(musicVolume) * 20f);
            musicVolumeSlider.value = musicVolume;
        }
    }

    public void OnVoicesVolumeSliderChanged(float level)
    {
        SetVoicesVolume(level);
    }

    public void OnSoundFXVolumeSliderChanged(float level)
    {
        SetSoundFXVolume(level);
    }

    public void OnMusicVolumeSliderChanged(float level)
    {
        SetMusicVolume(level);
    }
}


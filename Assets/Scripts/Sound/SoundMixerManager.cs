using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider VoicesVolumeSlider;
    [SerializeField] private Slider soundFXVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    [SerializeField]private TMP_Text VoicesText;
    [SerializeField]private TMP_Text SFXText;
    [SerializeField]private TMP_Text MusicText;

    private float musicVolume;
    private float voicesVolume;
    private float sfxVolume;

    private const string VoicesVolumeKey = "VoicesVolume";
    private const string SoundFXVolumeKey = "SoundFXVolume";
    private const string MusicVolumeKey = "MusicVolume";

    private string hexBrightRed = "#FF6666";
    private Color brightRedColor;

    void Start()
    {
        LoadVolumeSettings();
         if (ColorUtility.TryParseHtmlString(hexBrightRed, out brightRedColor)){}
         ChangeTextColor();
    }

    public void SetVoicesVolume(float level)
    {
        voicesVolume = level;
        audioMixer.SetFloat("VoicesVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat(VoicesVolumeKey, level);
        PlayerPrefs.Save();
        ChangeTextColor();
    }

    public void SetSoundFXVolume(float level)
    {
        sfxVolume = level;
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20f);
        audioMixer.SetFloat("soundFXVolume2", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat(SoundFXVolumeKey, level);
        PlayerPrefs.Save();
        ChangeTextColor();
    }

    public void SetMusicVolume(float level)
    {
        musicVolume = level;
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat(MusicVolumeKey, level);
        PlayerPrefs.Save();
        ChangeTextColor();
    }

    public void MuteVoices()
    {
        if(voicesVolume != 0.0001f)
        {
        audioMixer.SetFloat("VoicesVolume", 0.0001f);
        PlayerPrefs.SetFloat(VoicesVolumeKey, 0.0001f);
        voicesVolume = 0.0001f;
        VoicesVolumeSlider.value = voicesVolume;
        PlayerPrefs.Save();
        ChangeTextColor();
        }
        else if(voicesVolume == 0.0001f)
        {
        audioMixer.SetFloat("VoicesVolume", 1);
        PlayerPrefs.SetFloat(VoicesVolumeKey, 1);
        voicesVolume = 1f;
        VoicesVolumeSlider.value = voicesVolume;
        PlayerPrefs.Save();
        ChangeTextColor();
        }
    }

        public void MuteSFX()
    {
        if(sfxVolume != 0.0001f)
        {
        audioMixer.SetFloat("soundFXVolume", 0.0001f);
        audioMixer.SetFloat("soundFXVolume2", 0.0001f);
        PlayerPrefs.SetFloat(SoundFXVolumeKey, 0.0001f);
        sfxVolume = 0.0001f;
        soundFXVolumeSlider.value = sfxVolume;
        PlayerPrefs.Save();
        ChangeTextColor();
        }
        else if(sfxVolume == 0.0001f)
        {
        audioMixer.SetFloat("soundFXVolume", 1);
        audioMixer.SetFloat("soundFXVolume2", 1);
        PlayerPrefs.SetFloat(SoundFXVolumeKey, 1);
        sfxVolume = 1f;
        soundFXVolumeSlider.value = sfxVolume;
        PlayerPrefs.Save();
        ChangeTextColor();
        }
    }

        public void MuteMusic()
    {
        if(musicVolume != 0.0001f)
        {
        audioMixer.SetFloat("musicVolume", 0.0001f);
        PlayerPrefs.SetFloat(MusicVolumeKey, 0.0001f);
        musicVolume = 0.0001f;
        musicVolumeSlider.value = musicVolume;
        PlayerPrefs.Save();
        ChangeTextColor();
        }
        else if(musicVolume == 0.0001f)
        {
        audioMixer.SetFloat("musicVolume", 1);
        PlayerPrefs.SetFloat(MusicVolumeKey, 1);
        musicVolume = 1f;
        musicVolumeSlider.value = musicVolume;
        PlayerPrefs.Save();
        ChangeTextColor();
        }
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
            audioMixer.SetFloat("soundFXVolume2", Mathf.Log10(soundFXVolume) * 20f);
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

    public void ChangeTextColor()
    {
        if(voicesVolume == 0.0001f)
        {
            VoicesText.color = brightRedColor;;
        }
        else if(voicesVolume > 0.0001f)
        {
            VoicesText.color = Color.white;
        }

        if(sfxVolume == 0.0001f)
        {
            SFXText.color = brightRedColor;;
        }
        else if(sfxVolume > 0.0001f)
        {
            SFXText.color = Color.white;
        }

        if(musicVolume == 0.0001f)
        {
            MusicText.color = brightRedColor;;
        }
        else if(musicVolume > 0.0001f)
        {
            MusicText.color = Color.white;
        }
    }
}


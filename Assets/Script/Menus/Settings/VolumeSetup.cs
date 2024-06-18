using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetup : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider ambienceSlider;
    [SerializeField] private Slider sfxSlider;
    private float masterVolume;
    private float musicVolume;
    private float ambienceVolume;
    private float SFXVolume;

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("Master Volume", 0.5f);
        AudioManager.instance.masterVolume = masterSlider.value;

        musicSlider.value = PlayerPrefs.GetFloat("Music Volume", 0.5f);
        AudioManager.instance.musicVolume = musicSlider.value;

        ambienceSlider.value = PlayerPrefs.GetFloat("Ambience Volume", 0.5f);
        AudioManager.instance.ambienceVolume = ambienceSlider.value;

        sfxSlider.value = PlayerPrefs.GetFloat("SFX Volume", 0.5f);
        AudioManager.instance.SFXVolume = sfxSlider.value;
    }

    public void ChangeMasterSliderValue(float value)
    {
        masterSlider.value = value;
        PlayerPrefs.SetFloat("Master Volume", value);
        AudioManager.instance.masterVolume = masterSlider.value;
    }

    public void ChangeMusicSliderValue(float value)
    {
        musicSlider.value = value;
        PlayerPrefs.SetFloat("Music Volume", value);
        AudioManager.instance.musicVolume = musicSlider.value;
    }

    public void ChangeAmbienceSliderValue(float value)
    {
        ambienceSlider.value = value;
        PlayerPrefs.SetFloat("Ambience Volume", value);
        AudioManager.instance.ambienceVolume = ambienceSlider.value;
    }

    public void ChangeSFXSliderValue(float value)
    {
        sfxSlider.value = value;
        PlayerPrefs.SetFloat("SFX Volume", value);
        AudioManager.instance.SFXVolume = sfxSlider.value;
    }
}

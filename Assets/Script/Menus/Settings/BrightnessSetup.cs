using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessSetup : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Image brightnessPanel;
    private float sliderValue;

    private void Start()
    {
        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 0.5f);

        brightnessPanel.color = new Color(brightnessPanel.color.r,brightnessPanel.color.g, brightnessPanel.color.b, brightnessSlider.value);
    }

    public void ChangeSlider(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("Brightness", sliderValue);
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, sliderValue);
    }
}

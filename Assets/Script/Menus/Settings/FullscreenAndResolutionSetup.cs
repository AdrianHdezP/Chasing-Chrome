using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FullscreenAndResolutionSetup : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private TMP_Dropdown resolutionsDropdown;
    private Resolution[] resolutions;

    void Start()
    {
        if (Screen.fullScreen)
            toggle.isOn = true;
        else
            toggle.isOn = false;

        CheckResolution();
    }

    public void ChangeScreen(bool fullscreen) => Screen.fullScreen = fullscreen;

    private void CheckResolution()
    {
        resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();
        int actualResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x "  + resolutions[i].height;
            options.Add(option);

            if (Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height
                == Screen.currentResolution.height)
            {
                actualResolution = i;
            }
        }

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = actualResolution;
        resolutionsDropdown.RefreshShownValue();
    }

    public void ChangeResolution(int indexResolution)
    {
        Resolution resolution = resolutions[indexResolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}

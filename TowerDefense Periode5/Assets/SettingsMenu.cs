using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using TMPro;


public class SettingsMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject title;
    public GameObject settingsMenu;
    public GameObject credits;

    
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume",Mathf.Log10 (volume) * 20);
    }
    public void QualitySet(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void GoToSetting()
    {
        mainMenu.SetActive(false);
        title.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        title.SetActive(true);
        settingsMenu.SetActive(false);
    }
    public void BackToMainMenuFromCredits()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }
    public void ToCredis()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }
}

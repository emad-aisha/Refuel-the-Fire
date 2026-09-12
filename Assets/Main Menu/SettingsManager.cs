using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
    public static SettingsManager instance;
    [SerializeField] SavedSettings settingsToUpdate;

    [Header("Sound")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle fullscreenToggle;


    void Awake() {
        if (instance == null) instance = this;
        if (settingsToUpdate == null) settingsToUpdate = FindAnyObjectByType<SavedSettings>();

        UpdateMusicVolume();
        UpdateSFXVolume();
        SetResolution();
        SetFullscreen();
    }


    // SOUND
    public void UpdateMusicVolume() {
        settingsToUpdate.musicVolume = musicSlider.value / 10;
        AudioManager.instance.UpdateMusicVolume(settingsToUpdate.musicVolume);
    }
    public void UpdateSFXVolume() {
        settingsToUpdate.sfxVolume = sfxSlider.value / 10;
    }

    // RESO
    public void UpdateResolution() {
        // each literal value
        string value = resolutionDropdown.options[resolutionDropdown.value].text;
        string[] numbers = value.Split('x');

        settingsToUpdate.width = int.Parse(numbers[0]);
        settingsToUpdate.height = int.Parse(numbers[1]);

        SetResolution();
    }
    void SetResolution() { Screen.SetResolution(settingsToUpdate.width, settingsToUpdate.height, settingsToUpdate.fullscreenToggle); }

    public void ToggleFullscreen() {
        settingsToUpdate.fullscreenToggle = fullscreenToggle.isOn;
        SetFullscreen();
    }
    void SetFullscreen() { Screen.SetResolution(settingsToUpdate.width, settingsToUpdate.height, settingsToUpdate.fullscreenToggle); }



}

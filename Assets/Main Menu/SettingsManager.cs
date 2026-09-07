using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
    public static SettingsManager instance;
    [SerializeField] SavedSettings settingsToUpdate;

    [Header("Sound")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;


    void Awake() {
        if (instance == null) instance = this;

        UpdateMusicVolume();
        UpdateSFXVolume();
    }


    // SOUND
    public void UpdateMusicVolume() { settingsToUpdate.musicVolume = musicSlider.value / 10; }
    public void UpdateSFXVolume() { settingsToUpdate.sfxVolume = sfxSlider.value / 10; }

}

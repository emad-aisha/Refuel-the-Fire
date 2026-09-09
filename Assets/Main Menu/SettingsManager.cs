using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
    public static SettingsManager instance;
    [SerializeField] SavedSettings settingsToUpdate;

    [Header("Sound")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider squishSlider;


    void Awake() {
        if (instance == null) instance = this;

        UpdateMusicVolume();
        UpdateSFXVolume();
        UpdateSquishIntensity();
    }


    // SOUND
    public void UpdateMusicVolume() {
        Debug.Log("mus vol: " + musicSlider.value / 10);
        settingsToUpdate.musicVolume = musicSlider.value / 10;
        AudioManager.instance.UpdateMusicVolume(settingsToUpdate.musicVolume);
    }
    public void UpdateSFXVolume() {
        Debug.Log("sfx vol: " + sfxSlider.value / 10);
        settingsToUpdate.sfxVolume = sfxSlider.value / 10;
    }
    public void UpdateSquishIntensity() { settingsToUpdate.squishIntensity = squishSlider.value / 10; }

}

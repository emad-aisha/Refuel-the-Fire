using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    [SerializeField] SavedSettings savedSettings;

    [Header("SFX")]
    [SerializeField] List<AudioClip> clickClips;
    [SerializeField] AudioClip clip;

    [SerializeField, Range(0, 2)] float minPitch;
    [SerializeField, Range(0, 2)] float maxPitch;
    [SerializeField] AudioSource sfxAudioSource;

    [Header("Music")]
    [SerializeField] AudioClip music;
    [SerializeField] AudioSource musicAudioSource;


    void Awake() {
        if (instance == null) instance = this;
        savedSettings = FindAnyObjectByType<SavedSettings>();

        musicAudioSource.clip = music;
        musicAudioSource.volume = savedSettings.musicVolume;
        musicAudioSource.Play();
    }


    // SFX
    public void PlayClick() {
        sfxAudioSource.pitch = Random.Range(minPitch, maxPitch);
        sfxAudioSource.PlayOneShot(clickClips[RandomIndex()], savedSettings.sfxVolume);
    }

    public void PlayClock() {
        sfxAudioSource.pitch = Random.Range(minPitch, maxPitch);
        sfxAudioSource.PlayOneShot(clip, savedSettings.sfxVolume);
    }


    // MUSIC
    public void UpdateMusicVolume(float volume) {
        musicAudioSource.volume = volume;
    }

    int RandomIndex() {
        return Random.Range(0, clickClips.Count - 1);
    }


}

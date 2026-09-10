using UnityEngine;


public class SavedSettings : MonoBehaviour {
    public float musicVolume;
    public float sfxVolume;

    public int width = 1920;
    public int height = 1080;
    public bool fullscreenToggle = false;

    void Awake() {
        DontDestroyOnLoad(this);
        Screen.SetResolution(width, height, fullscreenToggle);
    }

}

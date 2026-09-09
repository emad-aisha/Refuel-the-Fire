using UnityEngine;


public class SavedSettings : MonoBehaviour {
    public float musicVolume;
    public float sfxVolume;
    public float squishIntensity;

    void Awake() {
        DontDestroyOnLoad(this);
    }

}

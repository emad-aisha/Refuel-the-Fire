using UnityEngine;


public class SavedSettings : MonoBehaviour {
    public float musicVolume;
    public float sfxVolume;

    void Awake() {
        DontDestroyOnLoad(this);
    }

}

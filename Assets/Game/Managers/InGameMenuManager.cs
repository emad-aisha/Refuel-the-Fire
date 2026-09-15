using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour {
    [SerializeField] string mainMenu = "Main Menu";
    [SerializeField] GameObject optionsMenu;
    SavedSettings savedSettings;

    [Header("Options")]
    [SerializeField] GameObject options;
    [SerializeField] GameObject sounds;
    [SerializeField] GameObject screen;
    [SerializeField] GameObject credit;

    [Header("Are U Sure")]
    [SerializeField] GameObject AreyouSureObject;

    float timescale;

    void Awake() {
        timescale = Time.timeScale;
        savedSettings = FindAnyObjectByType<SavedSettings>();
        HideAreYouSure();
        HideOptionsMenu();
    }

    // TODO: do a warning that ur stuff doesnt save
    public void ExitToMainMenu() { SceneManager.LoadSceneAsync(mainMenu); }

    // options
    public void ShowOptionsMenu() {
        Time.timeScale = 0;
        AudioManager.instance.UpdateMusicVolume(savedSettings.musicVolume - 0.1f);
        ShowMenu("FullMenu");
        ShowMenu("Options");
    }

    public void HideOptionsMenu() {
        AudioManager.instance.UpdateMusicVolume(savedSettings.musicVolume);
        Time.timeScale = timescale;
        ShowMenu();
    }

    public void ShowScreen() { ShowMenu("Screen"); }
    public void ShowSounds() { ShowMenu("Sounds"); }
    public void ShowCredits() { ShowMenu("Credits"); }
    public void ShowOptions() { ShowMenu("Options"); }

    public void ShowAreYouSure() { Time.timeScale = 0; AreyouSureObject.SetActive(true); }
    public void HideAreYouSure() { Time.timeScale = timescale; AreyouSureObject.SetActive(false); }

    public void ResetTime() { Time.timeScale = timescale; }

    void ShowMenu(string title = "") {
        if (title == "") optionsMenu.SetActive(false);
        options.SetActive(false);
        sounds.SetActive(false);
        screen.SetActive(false);
        credit.SetActive(false);

        switch (title) {
            case "FullMenu": optionsMenu.SetActive(true); break;
            case "Options": options.SetActive(true); break;
            case "Sounds": sounds.SetActive(true); break;
            case "Screen": screen.SetActive(true); break;
            case "Credits": credit.SetActive(true); break;
            default: break;
        }
    }

}

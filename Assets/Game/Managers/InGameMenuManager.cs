using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour {
    [SerializeField] string mainMenu = "Main Menu";
    [SerializeField] GameObject optionsMenu;

    [Header("Options")]
    [SerializeField] GameObject menu1;

    void Awake() {
        HideOptionsMenu();
    }

    // TODO: do a warning that ur stuff doesnt save
    public void ExitToMainMenu() { SceneManager.LoadSceneAsync(mainMenu); }

    // options
    public void ShowOptionsMenu() { optionsMenu.SetActive(true); }
    public void HideOptionsMenu() { optionsMenu.SetActive(false); }



}

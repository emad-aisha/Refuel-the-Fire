using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    [Header("Start Game")]
    [SerializeField] string sceneName;

    [Header("Menus")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;

    bool ignore;
    public void StartGame() { SceneManager.LoadSceneAsync(sceneName); }

    public void ShowOptionsMenu() {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }


    public void ShowMainMenu() {
        mainMenu.SetActive(true);

        optionsMenu.SetActive(false);
    }
}

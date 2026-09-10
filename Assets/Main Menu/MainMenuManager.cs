using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    readonly int NormalAnimation = Animator.StringToHash("Normal");

    [Header("Start Game")]
    [SerializeField] string sceneName;
    [SerializeField] List<Animator> allButtonAnimations;

    [Header("Menus")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject soundsMenu;
    [SerializeField] GameObject accessibilityMenu;
    [SerializeField] GameObject screenMenu;
    enum Menus { None = 0, Main = 1, Options = 2, Sounds = 3, Access = 4, Screen = 5 };

    bool ignore;
    public void StartGame() { SceneManager.LoadSceneAsync(sceneName); }

    // menu toggles
    public void ShowOptionsMenu() { ShowMenu(Menus.Options); }
    public void ShowMainMenu() { ShowMenu(Menus.Main); }
    public void ShowSoundsMenu() { ShowMenu(Menus.Sounds); }
    public void ShowAccessibilityMenu() { ShowMenu(Menus.Access); }
    public void ShowScreenMenu() { ShowMenu(Menus.Screen); }

    public void Quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }


    // helpers
    void ShowMenu(Menus menuToShow) {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(false);
        soundsMenu.SetActive(false);
        accessibilityMenu.SetActive(false);
        screenMenu.SetActive(false);

        switch (menuToShow) {
            case Menus.None: break;
            case Menus.Main: mainMenu.SetActive(true); break;
            case Menus.Options: optionsMenu.SetActive(true); break;
            case Menus.Sounds: soundsMenu.SetActive(true); break;
            case Menus.Access: accessibilityMenu.SetActive(true); break;
            case Menus.Screen: screenMenu.SetActive(true); break;
        }

        for (int i = 0; i < allButtonAnimations.Count; i++) {
            if (allButtonAnimations[i].isActiveAndEnabled) allButtonAnimations[i].Play(NormalAnimation);
        }
    }

}

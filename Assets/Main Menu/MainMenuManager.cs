using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MainMenuManager : MonoBehaviour {

    [Header("Start Game")]
    [SerializeField] string sceneName;
    [SerializeField] List<Animator> buttonAnimations;

    [Header("Menus")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    enum Menus { None = 0, Main = 1, Options = 2 };

    bool ignore;
    public void StartGame() { SceneManager.LoadSceneAsync(sceneName); }



    public void ShowOptionsMenu() {
        StartCoroutine(ShowMenu(Menus.Options));
    }
    public void ShowMainMenu() {
        mainMenu.SetActive(true);
        for (int i = 0; i < buttonAnimations.Count; i++) {
            buttonAnimations[i].Play("Normal");
        }
        // put other menus here?
        optionsMenu.SetActive(false);
    }



    public void Quit() {
        if (Application.isEditor) {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else {
            Application.Quit();
        }
    }


    // helpers
    IEnumerator ShowMenu(Menus menuToShow) {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(false);

        switch (menuToShow) {
            case Menus.None: break;
            case Menus.Main: mainMenu.SetActive(true); break;
            case Menus.Options: optionsMenu.SetActive(true); break;
        }

        yield break;
    }

}

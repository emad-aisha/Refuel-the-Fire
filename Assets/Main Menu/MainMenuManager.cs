using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

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
        // put other menus here?
        optionsMenu.SetActive(false);
    }



    [SerializeField] int safety = 500;


    IEnumerator ShowMenu(Menus menuToShow) {
        yield return StartCoroutine(WaitForAnimations());

        optionsMenu.SetActive(false);
        mainMenu.SetActive(false);

        switch (menuToShow) {
            case Menus.None: break;
            case Menus.Main: mainMenu.SetActive(true); break;
            case Menus.Options: optionsMenu.SetActive(true); break;
        }

    }

    // helper

    IEnumerator WaitForAnimations() {
        bool exit = false;
        bool isIdle = false;

        while (!exit && safety > 0) {
            yield return new WaitForFixedUpdate();
            for (int i = 0; i < buttonAnimations.Count; i++) {
                isIdle = !buttonAnimations[i].GetCurrentAnimatorStateInfo(0).IsName("Highlighted")
                          && !buttonAnimations[i].GetCurrentAnimatorStateInfo(0).IsName("Pressed");

                if (!isIdle) break;
            }

            if (isIdle) exit = true;
            safety--;
        }

    }

}

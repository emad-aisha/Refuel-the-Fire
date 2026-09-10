using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Unity.Mathematics;

public class MainMenuManager : MonoBehaviour {
    readonly int NormalAnimation = Animator.StringToHash("Normal");

    [Header("Start Game")]
    [SerializeField] string sceneName;
    [SerializeField] List<Animator> allButtonAnimations;

    [Header("Fade Data")]
    [SerializeField] Image fade;
    [SerializeField] float fadeTime = 1;
    [SerializeField] float waitTime = 0.5f;
    [SerializeField] bool loadWorld;

    [Header("Menus")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject soundsMenu;
    [SerializeField] GameObject accessibilityMenu;
    [SerializeField] GameObject screenMenu;
    [SerializeField] GameObject creditMenu;
    enum Menus { None = 0, Main = 1, Options = 2, Sounds = 3, Access = 4, Screen = 5, Credits = 6 };

    void Awake() {
        if (!loadWorld) StartCoroutine(FadeDuration(fadeTime, waitTime, loadWorld));
    }

    bool ignore;
    public void StartGame() {
        StartCoroutine(FadeDuration(fadeTime, waitTime, loadWorld));
    }

    // menu toggles
    public void ShowOptionsMenu() { ShowMenu(Menus.Options); }
    public void ShowMainMenu() { ShowMenu(Menus.Main); }
    public void ShowSoundsMenu() { ShowMenu(Menus.Sounds); }
    public void ShowAccessibilityMenu() { ShowMenu(Menus.Access); }
    public void ShowScreenMenu() { ShowMenu(Menus.Screen); }
    public void ShowCreditsMenu() { ShowMenu(Menus.Credits); }

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
        creditMenu.SetActive(false);

        switch (menuToShow) {
            case Menus.None: break;
            case Menus.Main: mainMenu.SetActive(true); break;
            case Menus.Options: optionsMenu.SetActive(true); break;
            case Menus.Sounds: soundsMenu.SetActive(true); break;
            case Menus.Access: accessibilityMenu.SetActive(true); break;
            case Menus.Screen: screenMenu.SetActive(true); break;
            case Menus.Credits: creditMenu.SetActive(true); break;
        }

        for (int i = 0; i < allButtonAnimations.Count; i++) {
            if (allButtonAnimations[i].isActiveAndEnabled) allButtonAnimations[i].Play(NormalAnimation);
        }
    }

    IEnumerator FadeDuration(float time, float waitTime, bool loadWorld) {
        float internalTime = 0;
        float oneMinus = loadWorld ? 0 : 1;

        while (internalTime <= time) {
            fade.color = new Color(0, 0, 0, math.abs(oneMinus - (internalTime / time)));
            Debug.Log(fade.color);
            internalTime += Time.deltaTime;
            yield return new WaitForSecondsRealtime(Time.deltaTime);
        }
        if (loadWorld) fade.color = new Color(0, 0, 0, 1);
        else fade.color = new Color(0, 0, 0, 0);

        yield return new WaitForSeconds(waitTime);

        if (loadWorld) SceneManager.LoadSceneAsync(sceneName);
        else Destroy(gameObject);
    }
}

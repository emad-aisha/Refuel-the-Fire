using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {
    [SerializeField] List<GameObject> UIObjects;
    [Header("")]
    [SerializeField] GameObject goodEnding;
    [SerializeField] GameObject badEnding;

    [Header("")]
    [SerializeField] GameObject goodInnerCasing;
    [SerializeField] GameObject badInnerCasing;


    void Start() {
        ProgressionChecker.OnComplete += ShowGoodEnding;
        HeatManager.instance.OnDeath += ShowBadEnding;
    }


    public void ShowGoodEnding() {
        ToggleUI(false);
        goodEnding.SetActive(true);
    }

    public void ShowBadEnding() {
        ToggleUI(false);
        badEnding.SetActive(true);
    }



    void ToggleUI(bool toggle) { foreach (GameObject ui in UIObjects) { ui.SetActive(toggle); } }

    public void ToggleGoodUI(bool toggle) { goodInnerCasing.SetActive(toggle); }
    public void ToggleBadUI(bool toggle) { badInnerCasing.SetActive(toggle); }

}

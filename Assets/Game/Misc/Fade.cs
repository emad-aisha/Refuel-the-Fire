using TMPro;
using UnityEngine;


public class Fade : MonoBehaviour {
    [SerializeField] TMP_Text text;
    [SerializeField] float fadeValue;

    void LateUpdate() {
        if (gameObject.activeSelf) {
            text.color -= new Color(0, 0, 0, fadeValue * Time.deltaTime);
        }
    }

}

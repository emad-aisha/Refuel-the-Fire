using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager instance;

    [SerializeField] Image heatBar;


    bool ignore;

    // SETUP
    void Awake() {
        if (instance == null) instance = this;
        else Destroy(this);
    }


    public void UpdateHeatBar(float percentFull) { heatBar.fillAmount = percentFull; }


}

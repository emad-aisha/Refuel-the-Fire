using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager instance;

    [Header("Currency")]
    [SerializeField] TMP_Text currency;
    [SerializeField] char moneySymbol = 'c';
    [SerializeField] bool addCommas;


    [Header("Heat")]
    [SerializeField] Image heatBar;


    bool ignore;

    // SETUP
    void Awake() {
        if (instance == null) instance = this;
        else Destroy(this);

        UpdateHeatBar(1);
        UpdateCurrency((int)StatManager.instance.GetPoints());
    }


    public void UpdateHeatBar(float percentFull) { heatBar.fillAmount = percentFull; }
    public void UpdateCurrency(int amountOfCurrency) {
        currency.text = addCommas switch {
            true => $"{amountOfCurrency:N0}", // :N formats for commas and the 0 means no decimal places
            _ => amountOfCurrency.ToString()
        };

        currency.text += moneySymbol;
    }

}

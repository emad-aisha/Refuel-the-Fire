using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour {
    public static CurrencyManager instance;

    [Header("Currency")]
    [SerializeField] TMP_Text currency;
    [SerializeField] public char moneySymbol = 'c';
    [SerializeField] bool addCommas;

    [Header("Heat")]
    [SerializeField] Image heatBar;

    void Awake() {
        if (instance == null) instance = this;

        UpdateHeatBar(1);
        UpdateCurrency((int)StatManager.instance.GetPoints());
    }

    public void UpdateHeatBar(float percentFull) { heatBar.fillAmount = percentFull; }
    public void UpdateCurrency(float amountOfCurrency) {
        // TODO: make this NOT show decimals once they reach a specific character length of moneys.

        currency.text = addCommas switch {
            true => amountOfCurrency.ToString("#,##0.##"),
            _ => amountOfCurrency.ToString()
        };

        currency.text += moneySymbol;
    }

}

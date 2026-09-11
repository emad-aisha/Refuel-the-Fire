using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour {
    public static CurrencyManager instance;

    [Header("Currency")]
    [SerializeField] TMP_Text currency;
    public char moneySymbol = '$';

    [Header("Popup")]
    [SerializeField] GameObject template;
    [SerializeField] float lifetime = 0.2f;

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

        // currency.text = amountOfCurrency.ToString("#,###.##");
        currency.text = FormatCurrency(amountOfCurrency);
    }

    public void PopUpCurrency(float value) {
        template.GetComponentInChildren<TMP_Text>().text = "+" + FormatCurrency(value);
        GameObject temp = Instantiate(template, transform);
        temp.SetActive(true);
        Destroy(temp, lifetime);
    }


    // helper
    string FormatCurrency(float value) { return string.Format("{0:#,##0.##}", value) + moneySymbol; ; }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager instance;

    [Header("Currency")]
    [SerializeField] TMP_Text currency;
    [SerializeField] public char moneySymbol = 'c';
    [SerializeField] bool addCommas;

    [Header("Heat")]
    [SerializeField] Image heatBar;


    [Header("Heat")]
    [SerializeField] GameObject sideBook;
    [SerializeField] int moveAmount;


    // SETUP
    void Awake() {
        if (instance == null) instance = this;
        else Destroy(this);

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


    public void HideSideBook() {
        sideBook.transform.position += new Vector3(moveAmount, 0);

        moveAmount = -moveAmount;
    }

}

using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Upgrade : MonoBehaviour {
    public enum Type { None = 0, Points = 1, Heat = 2 };
    public enum SubType { BasePoints, Multiplier, DecayRate };

    [Header("Basic Stats")]
    [SerializeField] protected UpgradeData upgradeData;

    //[Header("UI Stuffs")]
    TMP_Text costText;
    TMP_Text titleText;
    protected TMP_Text descText;
    protected Button button;

    public delegate void BoughtEvent();
    public static event BoughtEvent OnBuy;

    // SETUP
    virtual protected void Start() {
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text text in texts) {
            switch (text.gameObject.name) {
                case "Title": titleText = text; break;
                case "Price": costText = text; break;
                case "Desc": descText = text; break;
            }
        }

        button = GetComponent<Button>();
        button.onClick.AddListener(BuyUpgrade);
        SetUIData();

    }

    void OnEnable() { OnBuy += SetDesc; }
    void OnDisable() { OnBuy -= SetDesc; }

    void LateUpdate() {
        if (StatManager.instance.GetPoints() < upgradeData.cost) {
            button.interactable = false;
        }
        else if (button.interactable != true) {
            button.interactable = true;
        }
    }

    public virtual void BuyUpgrade() {
        if (StatManager.instance.Spend(upgradeData.cost)) {
            switch (upgradeData.upgradeType) {
                case Type.Points: UpgradePoints(); break;
                case Type.Heat: UpgradeHeat(); break;
            }

            UpdateData();
            AudioManager.instance.PlayClick();
            OnBuy?.Invoke();
        }
        else {
            Debug.Log("Not Enough Money");
        }
    }


    // UPGRADES
    void UpgradePoints() {
        switch (upgradeData.upgradeSubtype) {
            case SubType.BasePoints: StatManager.instance.AddBasePoints(upgradeData.upgradeAmount); break;
            case SubType.Multiplier: StatManager.instance.AddMultiplier(upgradeData.upgradeAmount); break;
        }
    }

    void UpgradeHeat() {
        switch (upgradeData.upgradeSubtype) {
            case SubType.BasePoints: HeatManager.instance.AddBaseValue(upgradeData.upgradeAmount); break;
            case SubType.Multiplier: HeatManager.instance.AddMultiplier(upgradeData.upgradeAmount); break;
            case SubType.DecayRate: HeatManager.instance.LowerDecay(upgradeData.upgradeAmount); break;
        }
    }

    // getters - for making new self
    void SetUIData() {
        titleText.text = upgradeData.title;
        costText.text = upgradeData.cost.ToString() + CurrencyManager.instance.moneySymbol;

        SetDesc();
    }

    virtual protected void SetDesc() {
        descText.text = upgradeData.upgradeType switch {
            Type.Heat => "Heat ",
            Type.Points => "Money ",
            _ => "N "
        };
        descText.text += upgradeData.upgradeSubtype switch {
            SubType.BasePoints => "",
            SubType.Multiplier => "Multiplier ",
            SubType.DecayRate => "Decay Rate ",
            _ => "X "
        };


        descText.text += upgradeData.upgradeSubtype switch {
            SubType.BasePoints => "+",
            SubType.Multiplier => "->",
            SubType.DecayRate => "-",
            _ => "~"
        };

        if (upgradeData.upgradeSubtype == SubType.Multiplier) {
            descText.text += upgradeData.upgradeType switch {
                Type.Heat => HeatManager.instance.GetMultiplier() + upgradeData.upgradeAmount,
                Type.Points => StatManager.instance.GetMultiplier() + upgradeData.upgradeAmount,
                _ => "null"
            };
        }
        else if ((upgradeData.upgradeSubtype == SubType.BasePoints && upgradeData.upgradeType == Type.Heat) || upgradeData.upgradeSubtype == SubType.DecayRate) {
            descText.text += math.abs(upgradeData.upgradeAmount) * 100;
        }
        else {
            descText.text += math.abs(upgradeData.upgradeAmount);
        }

        switch (upgradeData.upgradeType) {
            case Type.Heat: descText.color = SideBookManager.instance.heatColor; break;
            case Type.Points: descText.color = SideBookManager.instance.bpColor; break;
        }

    }


    protected virtual void UpdateData() {
        if (upgradeData.nextUpgrade == null) {
            Destroy(gameObject);
            return;
        }

        upgradeData = upgradeData.nextUpgrade;
        SetUIData();
    }

}

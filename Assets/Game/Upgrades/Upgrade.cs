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

        GetComponent<Button>().onClick.AddListener(BuyUpgrade);
        SetUIData();
    }

    public virtual void BuyUpgrade() {
        if (StatManager.instance.Spend(upgradeData.cost)) {
            switch (upgradeData.upgradeType) {
                case Type.Points: UpgradePoints(); break;
                case Type.Heat: UpgradeHeat(); break;
            }

            UpdateData();
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
            Type.Points => "Base Points ",
            _ => "N "
        };
        descText.text += upgradeData.upgradeSubtype switch {
            SubType.BasePoints => "",
            SubType.Multiplier => "Multiplier",
            SubType.DecayRate => "Decay Rate",
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
                Type.Points => HeatManager.instance.GetMultiplier() + upgradeData.upgradeAmount,
                _ => "null"
            };
        }
        else if (upgradeData.upgradeSubtype == SubType.DecayRate) {
            descText.text += math.abs(upgradeData.upgradeAmount) * 100; // to make it look better lmao :sob:
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

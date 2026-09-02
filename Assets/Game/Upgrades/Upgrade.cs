
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Click))]
public class Upgrade : MonoBehaviour {
    public enum Type { None = 0, Points = 1, Heat = 2 };
    public enum SubType { BasePoints, Multiplier, DecayRate };

    [Header("Basic Stats")]
    [SerializeField] UpgradeData upgradeData;

    [Header("UI Stuffs")]
    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text descText;



    // SETUP
    virtual protected void Start() {
        GetComponent<Click>().OnClick += BuyUpgrade;
        SetUIData();
    }

    void BuyUpgrade() {
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
    public void SetUIData() {
        titleText.text = upgradeData.title;
        costText.text = upgradeData.cost.ToString() + UIManager.instance.moneySymbol;
        descText.text = "\n" + upgradeData.description;
    }


    void UpdateData() {
        if (upgradeData.nextUpgrade == null) {
            Destroy(gameObject);
            return;
        }

        upgradeData = upgradeData.nextUpgrade;
        SetUIData();
    }

}


using TMPro;
using UnityEngine;

[RequireComponent(typeof(Click))]
public class Upgrade : MonoBehaviour {
    public enum Type { None = 0, Points = 1, Heat = 2 };
    public enum SubType { BasePoints, Multiplier, DecayRate };

    [Header("Basic Stats")]
    [SerializeField] float cost;
    [SerializeField] Type upgradeType;
    [SerializeField] SubType upgradeSubtype;
    [SerializeField] float upgradeAmount = 0;
    [SerializeField] Upgrade nextUpgrade = null;

    [Header("UI Stuffs")]
    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text descText;



    // SETUP
    virtual protected void Start() {
        GetComponent<Click>().OnClick += BuyUpgrade;
        costText.text = cost.ToString() + UIManager.instance.moneySymbol;
    }

    void BuyUpgrade() {
        if (StatManager.instance.Spend(cost)) {
            switch (upgradeType) {
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
        Debug.Log("Upgrade Points: " + upgradeSubtype.ToString());
        switch (upgradeSubtype) {
            case SubType.BasePoints: StatManager.instance.AddBasePoints(upgradeAmount); break;
            case SubType.Multiplier: StatManager.instance.AddMultiplier(upgradeAmount); break;
        }
    }

    void UpgradeHeat() {
        Debug.Log("Upgrade Heat: " + upgradeSubtype.ToString());
        switch (upgradeSubtype) {
            case SubType.BasePoints: HeatManager.instance.AddBaseValue(upgradeAmount); break;
            case SubType.Multiplier: HeatManager.instance.AddMultiplier(upgradeAmount); break;
            case SubType.DecayRate: HeatManager.instance.LowerDecay(upgradeAmount); break;
        }
    }

    // getters - for making new self
    public void SetUpgradeData(ref float newCost, ref float newUpgradeAmount, ref Upgrade newNextUpgrade) {
        newCost = cost;
        newUpgradeAmount = upgradeAmount;
        newNextUpgrade = nextUpgrade;
    }
    public void SetTypeData(ref Type newType, ref SubType newSubtype) {
        newType = upgradeType;
        newSubtype = upgradeSubtype;
    }
    public void SetUIData(ref TMP_Text newCostText, ref TMP_Text newTitleText, ref TMP_Text newDescText) {
        newTitleText.text = titleText.text;
        newCostText.text = costText.text;
        newDescText.text = descText.text;
    }

    void UpdateData() {
        if (nextUpgrade == null) {
            Destroy(gameObject);
            return;
        }

        nextUpgrade.SetTypeData(ref upgradeType, ref upgradeSubtype);
        nextUpgrade.SetUIData(ref costText, ref titleText, ref descText);
        nextUpgrade.SetUpgradeData(ref cost, ref upgradeAmount, ref nextUpgrade);

        costText.text = cost.ToString() + UIManager.instance.moneySymbol;
    }


}

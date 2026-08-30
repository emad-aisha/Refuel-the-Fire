
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Click))]
public class Upgrade : MonoBehaviour {
    public enum Type { None = 0, Points = 1, Heat = 2 };
    public enum SubType { BasePoints, Multiplier };

    [Header("Basic Stats")]
    [SerializeField] float cost;
    [SerializeField] Type upgradeType;
    [SerializeField] SubType upgradeSubtype;
    [SerializeField] float upgradeAmount = 0;
    [SerializeField] Upgrade nextUpgrade = null;

    [Header("UI Stuffs")]
    [SerializeField] TMP_Text costText;



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
            case SubType.BasePoints: StatManager.instance.AddBasePoints((ushort)upgradeAmount); break;
            case SubType.Multiplier: StatManager.instance.AddMultiplier(upgradeAmount); break;
        }
    }

    void UpgradeHeat() {
        Debug.Log("Upgrade Heat: " + upgradeSubtype.ToString());
        switch (upgradeSubtype) {
            case SubType.BasePoints: HeatManager.instance.AddBaseValue(upgradeAmount); break;
            case SubType.Multiplier: HeatManager.instance.AddMultiplier(upgradeAmount); break;
        }
    }

    // getters - for making new self
    public float GetCost() { return cost; }
    public new Type GetType() { return upgradeType; }
    public SubType GetSubType() { return upgradeSubtype; }
    public float GetUpgradeAmount() { return upgradeAmount; }
    public Upgrade GetNextUpgrade() { return nextUpgrade; }


    void UpdateData() {
        if (nextUpgrade == null) {
            Destroy(gameObject);
            return;
        }

        //newUpgrade
        cost = nextUpgrade.GetCost();
        upgradeType = nextUpgrade.GetType();
        upgradeSubtype = nextUpgrade.GetSubType();
        upgradeAmount = nextUpgrade.GetUpgradeAmount();
        nextUpgrade = nextUpgrade.GetNextUpgrade();

        costText.text = cost.ToString() + UIManager.instance.moneySymbol;
    }


}


using TMPro;
using UnityEngine;

[RequireComponent(typeof(Click))]
public class Upgrade : MonoBehaviour {
    public enum Type { Points, Heat };
    public enum SubType { BasePoints, Multiplier };

    [Header("Basic Stats")]
    [SerializeField] float cost;
    [SerializeField] Type upgradeType;
    [SerializeField] SubType upgradeSubtype;
    [SerializeField] float upgradeAmount = 1;

    [Header("UI Stuffs")]
    [SerializeField] TMP_Text costText;



    // SETUP
    void Start() {
        GetComponent<Click>().OnClick += BuyUpgrade;
        costText.text = cost.ToString() + UIManager.instance.moneySymbol;
    }

    void BuyUpgrade() {
        if (StatManager.instance.Spend(cost)) {
            switch (upgradeType) {
                case Type.Points: UpgradePoints(); break;
                case Type.Heat: UpgradeHeat(); break;
            }

            Destroy(gameObject);
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


}

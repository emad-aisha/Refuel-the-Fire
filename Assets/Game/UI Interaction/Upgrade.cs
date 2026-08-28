
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Click))]
public class Upgrade : MonoBehaviour {
    public enum Type { BasePoints, Multiplier };

    [Header("Basic Stats")]
    [SerializeField] float cost;
    [SerializeField] Type upgradeType;
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
                case Type.BasePoints: UpgradeBasePoints(); break;
                case Type.Multiplier: UpgradeMultiplier(); break;
                default: Debug.Log("Upgrade not set somehow"); break;
            }

            Destroy(gameObject);
        }
        else {
            Debug.Log("Not Enough Money");
        }
    }


    // UPGRADES
    void UpgradeBasePoints() {
        Debug.Log("Bought Base Point Upgrade");
        StatManager.instance.AddBasePoints((ushort)upgradeAmount);
    }

    void UpgradeMultiplier() {
        Debug.Log("Bought Multiplier Upgrade");
        StatManager.instance.AddMultiplier(upgradeAmount);
    }


}

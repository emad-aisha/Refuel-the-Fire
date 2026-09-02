using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Objects/UpgradeData")]
public class UpgradeData : ScriptableObject {
    [Header("Upgrade Data")]
    public Upgrade.Type upgradeType;
    public Upgrade.SubType upgradeSubtype;
    public float upgradeAmount;

    [Header("Price")]
    public float cost;

    [Header("Visual Stats")]
    public string title;
    public string description;

    [Header("Misc")]
    public UpgradeData nextUpgrade = null;
}

using UnityEngine;

[RequireComponent(typeof(Click))]
public class StatManager : MonoBehaviour {
    public static StatManager instance;

    [SerializeField] float totalPoints = 0;
    [SerializeField] uint basePoints = 1;
    [SerializeField] float multiplier = 1;


    // SETUP
    void Awake() {
        if (instance == null) instance = this;
        GetComponent<Click>().OnClick += AddPoints;
    }


    // HELPERS
    public void AddPoints() { totalPoints += basePoints * multiplier; Debug.Log("Points: " + totalPoints); }
    public void UpgradeBasePoints(ushort value) { basePoints += value; }
    public void UpgradeMultiplier(float value) { multiplier += value; }



    // TODO: make an import/export function using jsons
}

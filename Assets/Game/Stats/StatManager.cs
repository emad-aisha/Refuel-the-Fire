using UnityEngine;

[RequireComponent(typeof(Click))]
public class StatManager : MonoBehaviour {
    public static StatManager instance;

    [SerializeField] float totalPoints = 0;
    [SerializeField] float basePoints = 1;
    [SerializeField] float multiplier = 1;


    // SETUP
    void Awake() {
        if (instance == null) instance = this;
        GetComponent<Click>().OnClick += AddPoints;
    }


    // HELPERS
    public void AddPoints() {
        totalPoints += basePoints * multiplier;
        CurrencyManager.instance.UpdateCurrency(totalPoints);
    }

    public void AddBasePoints(float value) { basePoints += value; }
    public void AddMultiplier(float value) { multiplier += value; }

    public float GetMultiplier() { return multiplier; }

    public bool Spend(float cost) {
        if (totalPoints - cost < 0) {
            return false;
        }
        else {
            totalPoints -= cost;
            CurrencyManager.instance.UpdateCurrency(totalPoints);
            return true;
        }
    }

    public float GetPoints() { return totalPoints; }

    // TODO: make an import/export function using jsons
}

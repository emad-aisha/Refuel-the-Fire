using UnityEngine;

[RequireComponent(typeof(Click))]
public class HeatManager : MonoBehaviour {
    public static HeatManager instance;

    [SerializeField, Range(0, 0.1f)] float decayRate = 0.1f;
    [SerializeField, Range(0, 0.1f)] float baseValue = 0.1f; // this need a way to upgrade

    float totalHeat = 1; // starts at full
    float multiplier = 1;

    public delegate void DieEvent();
    public event DieEvent OnDeath;


    // SETUP
    void Awake() {
        if (instance == null) instance = this;
        GetComponent<Click>().OnClick += AddHeat;
    }

    void OnDisable() { OnDeath = null; }

    void Update() {
        // todo: make it wait a little before decaying the heat again (looks better + nice)
        totalHeat -= decayRate * Time.deltaTime;
        UIManager.instance.UpdateHeatBar(totalHeat);
        DeathCheck();
    }


    public void AddBaseValue(float value) { baseValue += value; }
    public void AddMultiplier(float value) { multiplier += value; }
    public void LowerDecay(float value) { decayRate -= value; }


    void AddHeat() { totalHeat += baseValue * multiplier; if (totalHeat > 1) totalHeat = 1; }
    void DeathCheck() {
        if (totalHeat <= 0) {
            Debug.Log("Dead...");
            OnDeath?.Invoke();
            enabled = false;
        }
    }


}

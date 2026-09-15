using System.Collections;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Click))]
public class HeatManager : MonoBehaviour {
    public static HeatManager instance;

    [Header("Basic Stats")]
    [SerializeField, Range(0, 0.1f)] float decayRate = 0.1f;
    [SerializeField, Range(0, 0.1f)] float baseValue = 0.1f; // this need a way to upgrade

    [Header("Lerp")]
    [SerializeField] float pauseTime = 0.1f;
    [SerializeField] float delayTime = 0.1f;

    [SerializeField] GameObject heatObject;
    [SerializeField] GameObject smokeObject;

    float totalHeat = 1; // starts at full
    float multiplier = 1;

    public delegate void DieEvent();
    public event DieEvent OnDeath;
    bool isPaused = false;
    float preAdd;
    float postAdd;
    float ignoreVel;


    // SETUP
    void Awake() {
        if (instance == null) instance = this;
        GetComponent<Click>().OnClick += AddHeat;
    }

    void OnDisable() { OnDeath = null; }

    void Update() {
        if (isPaused) {
            preAdd = Mathf.SmoothDamp(preAdd, postAdd, ref ignoreVel, delayTime);
            if (preAdd > 1) preAdd = 1;
            CurrencyManager.instance.UpdateHeatBar(preAdd);
            return;
        }

        // todo: make it wait a little before decaying the heat again (looks better + nice)
        totalHeat -= decayRate * Time.deltaTime;
        CurrencyManager.instance.UpdateHeatBar(totalHeat);
        DeathCheck();
    }


    public void AddBaseValue(float value) { baseValue += value; }
    public void AddMultiplier(float value) { multiplier += value; }
    public void LowerDecay(float value) { decayRate = Mathf.Clamp(decayRate - value, 0, 1); }

    public float GetMultiplier() { return multiplier; }

    void AddHeat() {
        preAdd = totalHeat;
        totalHeat += baseValue * multiplier;
        postAdd = totalHeat;

        if (totalHeat > 1) totalHeat = 1;
        CurrencyManager.instance.UpdateHeatBar(totalHeat);
        StartCoroutine(Pause(pauseTime));
    }
    void DeathCheck() {
        if (totalHeat <= 0) {
            Debug.Log("Dead...");
            heatObject.SetActive(false);
            smokeObject.SetActive(false);
            OnDeath?.Invoke();
            enabled = false;
        }
    }

    IEnumerator Pause(float time) {
        isPaused = true;
        yield return new WaitForSeconds(time);
        isPaused = false;
    }

}

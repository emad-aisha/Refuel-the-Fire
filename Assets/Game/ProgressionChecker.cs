using UnityEngine;

public class ProgressionChecker : MonoBehaviour {
    int totalUpgrades = -1;

    public delegate void CompletedEvent();
    public static event CompletedEvent OnComplete;


    void LateUpdate() {
        if (totalUpgrades == -1) return;

        Debug.Log("total: " + totalUpgrades);

        if (FinishedGame()) {
            HeatManager.instance.enabled = false;
            Debug.Log("finished game");
            totalUpgrades = -1;
            OnComplete?.Invoke();
        }
    }

    void OnDisable() { OnComplete = null; }

    bool FinishedGame() { return totalUpgrades == 0; }

    public void IncrementTotalUpgrades() {
        if (totalUpgrades == -1) totalUpgrades = 0;
        totalUpgrades++;
    }
    public void DecrementTotalUpgrades() {
        totalUpgrades--;
    }

}

using UnityEngine;

public class ProgressionChecker : MonoBehaviour {
    int totalUpgrades = -1;

    public delegate void CompletedEvent();
    public event CompletedEvent OnComplete;


    void LateUpdate() {
        if (totalUpgrades == -1) return;

        if (FinishedGame()) {
            Debug.Log("finished game");
            totalUpgrades = -1;
            OnComplete?.Invoke();
        }
    }

    void OnDisable() { OnComplete = null; }

    bool FinishedGame() { return totalUpgrades == 0; }

    public void IncrementTotalUpgrades() { totalUpgrades++; }
    public void DecrementTotalUpgrades() { totalUpgrades--; }

}

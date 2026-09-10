using UnityEngine;
using UnityEngine.UI;

public class SideBookManager : MonoBehaviour {

    [Header("Side Book")]
    [SerializeField] GameObject sideBook;
    [SerializeField] int moveAmount;
    [SerializeField] float dampTime = 0.5f;

    [Header("Tabs")]
    [SerializeField] Image upgradeTabs;
    [SerializeField] Image progressTabs;
    [SerializeField] Color baseColor;

    [Header("Panels")]
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject progressPanel;
    Vector3 originalPosition;
    Vector3 ignoredVelocity;
    int internalMoveAmount = 0;

    // SETUP
    void Awake() {
        originalPosition = sideBook.transform.position;
        baseColor = progressTabs.color;

        ShowUpgrades();
        CloseSideBook();
    }

    void Update() {
        sideBook.transform.position = Vector3.SmoothDamp(sideBook.transform.position, originalPosition + new Vector3(0, internalMoveAmount), ref ignoredVelocity, dampTime);
    }


    // sidebook
    public void ToggleSideBook() {
        //Vector3.MoveTowards(originalPosition, originalPosition + new Vector3(0, moveAmount), 0.1f);
        //sideBook.transform.position += new Vector3(0, moveAmount);
        if (internalMoveAmount == 0) internalMoveAmount = moveAmount;
        else internalMoveAmount = 0;
    }
    public void ShowProgress() {
        upgradeTabs.color = baseColor - new Color(0.1f, 0.1f, 0.1f, 0);
        progressTabs.color = baseColor;

        progressPanel.SetActive(true);
        upgradePanel.SetActive(false);
    }
    public void ShowUpgrades() {
        upgradeTabs.color = baseColor;
        progressTabs.color = baseColor - new Color(0.1f, 0.1f, 0.1f, 0);

        upgradePanel.SetActive(true);
        progressPanel.SetActive(false);
    }

    void CloseSideBook() {
        ToggleSideBook();
        sideBook.transform.position = originalPosition + new Vector3(0, internalMoveAmount);
    }


}

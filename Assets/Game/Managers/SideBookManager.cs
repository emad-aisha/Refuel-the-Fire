using UnityEngine;
using UnityEngine.UI;

public class SideBookManager : MonoBehaviour {
    public static SideBookManager instance;

    [Header("Side Book")]
    [SerializeField] GameObject sideBook;
    [SerializeField] float dampTime = 0.5f;

    [Header("Tabs")]
    [SerializeField] Image upgradeTabs;
    [SerializeField] Image progressTabs;
    Color baseColor;

    [Header("Upgrade Colors")]
    [SerializeField] public Color heatColor;
    [SerializeField] public Color bpColor;

    [Header("Panels")]
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject progressPanel;
    Vector2 ignoredVelocity;
    float internalMoveAmount = 0.5f;
    float originalYPivot;

    RectTransform sideBookTransform;

    // SETUP
    void Awake() {
        if (!instance) instance = this;

        baseColor = progressTabs.color;
        sideBookTransform = sideBook.GetComponent<RectTransform>();
        originalYPivot = sideBookTransform.pivot.y;

        ShowProgress();
        ShowUpgrades();
        CloseSideBook();
    }

    void Update() {
        sideBookTransform.pivot = Vector2.SmoothDamp(sideBookTransform.pivot, new Vector2(0.5f, internalMoveAmount), ref ignoredVelocity, dampTime);
    }


    // sidebook
    public void ToggleSideBook() {
        //Vector3.MoveTowards(originalPosition, originalPosition + new Vector3(0, moveAmount), 0.1f);
        //sideBook.transform.position += new Vector3(0, moveAmount);
        if (internalMoveAmount == originalYPivot) internalMoveAmount = 1;
        else internalMoveAmount = originalYPivot;
    }
    public void ShowProgress() {
        if (internalMoveAmount != originalYPivot) { ToggleSideBook(); }

        upgradeTabs.color = baseColor - new Color(0.1f, 0.1f, 0.1f, 0);
        progressTabs.color = baseColor;

        progressPanel.SetActive(true);
        upgradePanel.SetActive(false);
    }
    public void ShowUpgrades() {
        if (internalMoveAmount != originalYPivot) { ToggleSideBook(); }

        upgradeTabs.color = baseColor;
        progressTabs.color = baseColor - new Color(0.1f, 0.1f, 0.1f, 0);

        upgradePanel.SetActive(true);
        progressPanel.SetActive(false);
    }

    void CloseSideBook() {
        ToggleSideBook();
    }

}

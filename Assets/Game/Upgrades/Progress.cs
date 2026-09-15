using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Progress : Upgrade {

    [Header("Progression Stats")]
    [SerializeField] UnityEvent increment;
    [SerializeField] UnityEvent decrement;
    [SerializeField] public List<GameObject> objects;
    int internalIndex = 0;

    override protected void Start() {
        base.Start();
        increment?.Invoke();
    }

    void LateUpdate() {
        if (StatManager.instance.GetPoints() < upgradeData.cost) {
            button.interactable = false;
        }
        else {
            button.interactable = true;
        }
    }

    public override void BuyUpgrade() {
        if (StatManager.instance.Spend(upgradeData.cost)) {
            if (objects[internalIndex] != null) objects[internalIndex].SetActive(true);
            decrement?.Invoke();
            UpdateData();
            AudioManager.instance.PlayClick();
        }
        else {
            Debug.Log("Not Enough Money");
        }
    }

    protected override void UpdateData() {
        base.UpdateData();
        internalIndex++;
        if (internalIndex < objects.Count) increment?.Invoke();
    }


    override protected void SetDesc() {
        descText.text = "+1 " + upgradeData.description;
    }
}

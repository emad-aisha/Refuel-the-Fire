using System.Collections.Generic;
using UnityEngine;

public class Progress : Upgrade {

    [Header("Progression Stats")]
    [SerializeField] public List<GameObject> objects;
    int internalIndex = 0;

    override protected void Start() {
        base.Start();
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
    }


    override protected void SetDesc() {
        descText.text = "+1 " + upgradeData.description;
    }
}

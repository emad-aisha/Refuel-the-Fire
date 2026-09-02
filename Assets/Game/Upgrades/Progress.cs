using UnityEngine;

public class Progress : Upgrade {

    [Header("Progression Stats")]
    [SerializeField] GameObject objectToShow;

    override protected void Start() {
        base.Start();
    }


    public void BuyObject() {
        if (objectToShow != null) objectToShow.SetActive(true);
        Destroy(gameObject);
    }


}

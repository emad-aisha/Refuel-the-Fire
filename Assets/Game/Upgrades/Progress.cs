using UnityEngine;

[RequireComponent(typeof(Click))]
public class Progress : Upgrade {

    [Header("Progression Stats")]
    [SerializeField] GameObject objectToShow;

    override protected void Start() {
        base.Start();
        GetComponent<Click>().OnClick += BuyObject;
    }


    void BuyObject() {
        if (objectToShow != null) objectToShow.SetActive(true);
        Destroy(gameObject);
    }


}

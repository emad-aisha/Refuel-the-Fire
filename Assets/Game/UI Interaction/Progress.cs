using UnityEngine;

[RequireComponent(typeof(Click))]
public class Progress : Upgrade {

    [Header("Progression Stats")]
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] Vector3 positionToSpawn;

    override protected void Start() {
        base.Start();
        GetComponent<Click>().OnClick += BuyObject;
    }


    void BuyObject() {
        Instantiate(objectToSpawn, positionToSpawn, Quaternion.identity);
        Destroy(gameObject);
    }


}

using UnityEngine;

[RequireComponent(typeof(Click))]
public class Upgrade : MonoBehaviour {

    [Header("Basic Stats")]
    [SerializeField] float cost;

    // SETUP
    void Start() {
        GetComponent<Click>().OnClick += BuyUpgrade;
    }


    void BuyUpgrade() {
        if (StatManager.instance.Spend(cost)) {
            Debug.Log("Bought Upgrade");
            Destroy(gameObject);
        }
        else {
            Debug.Log("Not Enough Money");
        }
    }

}

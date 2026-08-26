using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionManager : MonoBehaviour {
    [SerializeField] InputActionAsset inputAsset;
    [SerializeField] string actionMap;

    public static InputActionManager instance;

    // SETUP
    void OnEnable() { inputAsset.FindActionMap(actionMap).Enable(); }
    void OnDisable() { inputAsset.FindActionMap(actionMap).Disable(); }
    void Awake() {
        if (instance == null) instance = this;
        else Destroy(this);
    }


    // HELPERS
    public InputAction GetAction(string actionToFind) { return inputAsset.FindActionMap(actionMap).FindAction(actionToFind); }

}

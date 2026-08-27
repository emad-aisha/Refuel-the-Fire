using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform))]
public class Click : MonoBehaviour {
    public delegate void ClickEvent();
    public event ClickEvent OnClick;

    InputAction clickAction;
    RectTransform bounds;

    // SETUP
    void Start() {
        bounds = GetComponent<RectTransform>();
        clickAction = InputActionManager.instance.GetAction("Click");
        clickAction.performed += Clicked;
    }

    void OnDisable() {
        OnClick = null;
        clickAction.performed -= Clicked;
    }

    // EVENT
    void Clicked(InputAction.CallbackContext context) {
        // TODO: add a micro cooldown
        if (RectTransformUtility.RectangleContainsScreenPoint(bounds, context.ReadValue<Vector2>())) {
            Debug.Log("Clicked: " + name);
            OnClick?.Invoke();
        }
    }

}

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
        OnClick += Squish;
    }

    void OnDisable() {
        OnClick = null;
        clickAction.performed -= Clicked;
    }

    // CLICK EVENT
    void Clicked(InputAction.CallbackContext context) {
        if (RectTransformUtility.RectangleContainsScreenPoint(bounds, context.ReadValue<Vector2>())) OnClick?.Invoke();
    }
    void OnMouseDown() { OnClick?.Invoke(); }

    void Squish() { if (TryGetComponent(out Animator animator)) animator.SetTrigger(Animator.StringToHash("Clicked")); }


}

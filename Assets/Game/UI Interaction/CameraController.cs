using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour {

    [Header("Basic Values")]
    [SerializeField] float cap = 2;
    [SerializeField] float moveAmount = 5;
    [SerializeField] float delay = 1;


    InputAction mousePositionAction;
    Vector3 originalCameraPosition;
    Vector3 currentVelocity;

    void Start() {
        originalCameraPosition = transform.position;
        mousePositionAction = InputActionManager.instance.GetAction("Mouse Position");
    }


    void LateUpdate() {
        transform.position = Vector3.SmoothDamp(transform.position, GetNewPosition(), ref currentVelocity, delay);
    }


    Vector3 GetNewPosition() {
        Vector3 lerpedPos = new(GetMousePositionAsFloat().x, GetMousePositionAsFloat().y);

        Vector3 originalValue = originalCameraPosition + (lerpedPos * moveAmount);
        originalValue.x = Mathf.Clamp(originalValue.x, -cap, cap);
        originalValue.y = Mathf.Clamp(originalValue.y, -cap, cap);

        return originalValue;
    }

    Vector2 GetMousePositionAsFloat() {
        Vector2 result = mousePositionAction.ReadValue<Vector2>();
        result.x = (result.x / Screen.width) - 0.5f;
        result.y = (result.y / Screen.height) - 0.5f;

        return result;
    }


}

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraZoom : MonoBehaviour {

    [Header("Basic Stats")]
    [SerializeField] float zoomScale = 1;
    [SerializeField] float delay = 0.2f;
    Camera currentCamera;
    InputAction rightClickAction;
    float originalSize;

    float velocity;

    void Awake() {
        currentCamera = GetComponent<Camera>();
        originalSize = currentCamera.orthographicSize;

        rightClickAction = InputActionManager.instance.GetAction("Right Click");
    }

    void LateUpdate() {
        currentCamera.orthographicSize = Mathf.SmoothDamp(currentCamera.orthographicSize, NewCameraZoom(), ref velocity, delay);
    }

    float NewCameraZoom() { return originalSize += ZoomDelta(); }
    float ZoomDelta() {
        if (rightClickAction.WasPerformedThisFrame()) {
            zoomScale = -zoomScale;
            return zoomScale;
        }
        else {
            return 0;
        }
    }
}

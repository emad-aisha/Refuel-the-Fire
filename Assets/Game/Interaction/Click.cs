using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform))]
public class Click : MonoBehaviour {
    public delegate void ClickEvent();
    public event ClickEvent OnClick;

    [SerializeField] ParticleSystem particles;
    InputAction clickAction;
    RectTransform bounds;

    // SETUP
    void Start() {
        bounds = GetComponent<RectTransform>();
        clickAction = InputActionManager.instance.GetAction("Click");
        OnClick += Squish;
    }

    void OnDisable() {
        OnClick = null;
    }


    void OnMouseDown() {
        OnClick?.Invoke();
        ParticleSystem particleSystem = Instantiate(particles, transform);
        particleSystem.Play();
        Destroy(particleSystem.gameObject, 0.5f);

        //particles.Play();
        AudioManager.instance.PlayClick();
    }

    void Squish() { if (TryGetComponent(out Animator animator)) animator.SetTrigger(Animator.StringToHash("Clicked")); }


}

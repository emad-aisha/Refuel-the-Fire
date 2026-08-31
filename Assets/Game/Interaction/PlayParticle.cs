using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PlayParticle : MonoBehaviour {
    //[Header("")]
    [SerializeField] Click click;

    void Start() {
        click.OnClick += GetComponent<ParticleSystem>().Play;
    }



}

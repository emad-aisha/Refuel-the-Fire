using UnityEngine;
using System.Collections.Generic;

public class SetSortingLayers : MonoBehaviour {
    List<SpriteRenderer> sprites = new();


    [ContextMenu("Set Sorting Layers")]
    public void FixSortingLayers() {
        sprites.AddRange(GetComponentsInChildren<SpriteRenderer>());

        foreach (SpriteRenderer sprite in sprites) {
            sprite.sortingOrder = (int)(-sprite.gameObject.transform.position.z * 100);
        }

    }

}

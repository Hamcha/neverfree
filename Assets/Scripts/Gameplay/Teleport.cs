using UnityEngine;

public class Teleport : MonoBehaviour {
    private RangeSprite range;

    void Awake() {
        range = GetComponent<RangeSprite>();
    }

    void FixedUpdate() {
        bool isPlayerClose = Vector2.Distance(Cursor.instance.transform.position, transform.position) < range.currentScale.x/2;
        Cursor.instance.Teleport(isPlayerClose);

    }
}

using UnityEngine;

public class Phantom : MonoBehaviour {
    public SpriteRenderer body, mane;

    void Start() {
        // Load customized pony
        mane.sprite = Scene.instance.storage.maneStyles[Player.Instance.data.maneStyle];
        mane.color = Player.Instance.data.maneColor;
        body.color = Player.Instance.data.bodyColor;

        float direction = Mathf.Sign(Scene.gui.cursor.transform.position.x - transform.position.x);
        Vector3 rScale = transform.localScale;
        rScale.x = direction;
        transform.localScale = rScale;
    }
}

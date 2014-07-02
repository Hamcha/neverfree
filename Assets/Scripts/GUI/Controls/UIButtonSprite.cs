using UnityEngine;

public class UIButtonSprite : MonoBehaviour {

    public Sprite idle, focus, pressed, hold;
    private UIButton button;
    private SpriteRenderer render;

    void Awake() {
        button = GetComponent<UIButton>();
        render = GetComponent<SpriteRenderer>();
        button.StatusChanged += (_, status) => {
            if (status == UIButton.ButtonStatus.Idle) {
                render.sprite = idle;
            } else if (status == UIButton.ButtonStatus.Focused && focus != null) {
                render.sprite = focus;
            } else if (status == UIButton.ButtonStatus.Pressed && button.hold && hold != null) {
                render.sprite = hold;
            } else if (status == UIButton.ButtonStatus.Pressed && !button.hold && pressed != null) {
                render.sprite = pressed;
            }
        };
        render.sprite = idle;
    }
}

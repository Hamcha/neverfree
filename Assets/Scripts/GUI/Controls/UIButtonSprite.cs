using UnityEngine;
using System.Collections;

public class UIButtonSprite : MonoBehaviour {

    public Sprite idle, focus, active, hold;
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
            } else if (status == UIButton.ButtonStatus.Pressed && !button.hold && active != null) {
                render.sprite = active;
            }
        };
        render.sprite = idle;
    }
}

using UnityEngine;
using System.Collections;

public class UIButtonSprite : MonoBehaviour {

    public Sprite idle, focus, active, hold;
    private UIButton button;
    private SpriteRenderer render;

	void Awake() {
        button = GetComponent<UIButton>();
        button.OnFocus += (_) => {
            if (focus != null) render.sprite = focus;
        };
        button.OnBlur += (_) => {
            render.sprite = idle;
        };
        button.Clicked += (_) => {
            if (button.hold && hold != null) {
                render.sprite = button.status == UIButton.ButtonStatus.Pressed ? hold : idle;
            } else {
                if (active != null) render.sprite = active;
            }
        };
        render.sprite = idle;
	}
}

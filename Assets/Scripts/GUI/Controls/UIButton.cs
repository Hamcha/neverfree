using UnityEngine;
using System.Collections;

public class UIButton : MonoBehaviour {
    public delegate void ClickedHandler(UIButton instance);
    public event ClickedHandler Clicked;
    public event ClickedHandler OnFocus, OnBlur;

    public bool enabled = true;
    public bool hold = false;
    public ButtonStatus status = ButtonStatus.Idle;

    public enum ButtonStatus {
        Idle,
        Focused,
        Pressed
    };

    void OnMouseDown() {
        if (!enabled) return;

        if (status != ButtonStatus.Pressed) {
            status = ButtonStatus.Pressed;
            if (Clicked != null) Clicked(this);
        } else if (hold) {
            status = ButtonStatus.Idle;
            if (Clicked != null) Clicked(this);
        }
    }

    void OnMouseUp() {
        if (!enabled) return;

        if (status == ButtonStatus.Pressed && !hold) {
            status = ButtonStatus.Idle;
        }
    }

    void OnMouseEnter() {
        if (OnFocus != null) OnFocus(this);
    }

    void OnMouseLeave() {
        if (OnBlur != null) OnBlur(this);
    }
}

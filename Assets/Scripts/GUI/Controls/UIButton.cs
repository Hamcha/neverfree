using UnityEngine;

public class UIButton : MonoBehaviour {
    public delegate void ClickedHandler(UIButton instance);
    public event ClickedHandler Clicked;
    public event ClickedHandler OnFocus, OnBlur;

    public delegate void StatusChangedHandler(UIButton instance, ButtonStatus status);
    public event StatusChangedHandler StatusChanged;

    public bool disabled = false;
    public bool hold = false;

    private ButtonStatus _status = ButtonStatus.Idle;
    public ButtonStatus status {
        get { return _status; }
        set { _status = value; if (StatusChanged != null) StatusChanged(this, value); }
    }

    public enum ButtonStatus {
        Idle,
        Focused,
        Pressed
    };

    void OnMouseDown() {
        if (disabled) return;

        if (status != ButtonStatus.Pressed) {
            status = ButtonStatus.Pressed;
        } else if (hold) {
            status = ButtonStatus.Idle;
            if (Clicked != null) Clicked(this);
        }
    }

    void OnMouseUpAsButton() {
        if (disabled) return;

        if (status == ButtonStatus.Pressed && !hold) {
            status = ButtonStatus.Idle;
            if (Clicked != null) Clicked(this);
        }
    }

    void OnMouseEnter() {
        if (status != ButtonStatus.Pressed) {
            status = ButtonStatus.Focused;
            if (OnFocus != null) OnFocus(this);
        }
    }

    void OnMouseExit() {
        if (!hold && status != ButtonStatus.Focused) {
            status = ButtonStatus.Idle;
            if (OnBlur != null) OnBlur(this);
        }
    }
}

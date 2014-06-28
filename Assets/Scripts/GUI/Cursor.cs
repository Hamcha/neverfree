using UnityEngine;

public class Cursor : MonoBehaviour {
    static Cursor cursorInstance;

    private Animator animator;
    public string currentType = "none";

    Cursor() {
        cursorInstance = this;
    }

    void Awake() {
        // Disable hardware cursor
        Screen.showCursor = false;
        animator = GetComponent<Animator>();
    }

    void Update() {
        // Get mouse coordinate on screen
        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        p.z = -9;
        // Set GUI cursor to mouse cursor position
        transform.position = p;
    }

    static public void setCursorType(string type) {
        // Skip if already current type
        if (type == cursorInstance.currentType) return;
        cursorInstance.currentType = type;

        if (type == "BaseShot") {
            cursorInstance.animator.SetBool("Aiming", true);
        } else {
            cursorInstance.animator.SetBool("Aiming", false);
        }
    }
}
using UnityEngine;

public class Cursor : MonoBehaviour {
    public static Cursor instance;

    public Animator animator;
    public string currentType = "Inspect";
    public string currentStance = "Inspect";

    public GameObject highlighted;

    void Awake() {
        instance = this;
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

    public void SetCursorStance(string stance) {
        currentStance = stance;
        animator.SetBool("Aiming", stance == "BaseShot");
    }

    public void Reset() {
        currentType = "";
        animator.SetBool("Interactive", false);
    }

    public void Highlight(GameObject obj) {
        highlighted = obj;
        animator.SetBool("Interactive", true);
    }

    public void Blur(GameObject obj) {
        if (highlighted == obj) {
            highlighted = null;
            Reset();
        }
    }

    public void CloseTo(GameObject obj) {
        Debug.Log("Got close");
        if (highlighted == obj) animator.SetBool("Near", true);
    }

    public void FarTo(GameObject obj) {
        if (highlighted == obj) animator.SetBool("Near", false);
    }

    public void OnDestroy() {
        if (instance == this) instance = null;
    }
}
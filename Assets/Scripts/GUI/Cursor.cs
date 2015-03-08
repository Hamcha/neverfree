using UnityEngine;

public class Cursor : MonoBehaviour {
    public static Cursor instance;

    public Animator animator;
    public string currentType = "Inspect";
    public string currentAbility = "Inspect";

    public GameObject highlighted;

    void Awake() {
        instance = this;
        // Disable hardware cursor
        UnityEngine.Cursor.visible = false;
        animator = GetComponent<Animator>();
    }

    void Update() {
        // Get mouse coordinate on screen
        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        p.z = -9;
        // Set GUI cursor to mouse cursor position
        transform.position = p;
    }

    public void SetCursorAbility(string ability) {
        if (!gameObject.activeSelf) return;
        currentAbility = ability;
        animator.SetBool("Aiming", ability == "BaseShot");
    }

    public void Reset() {
        if (!gameObject.activeSelf) return;
        currentType = "";
        animator.SetBool("Interactive", false);
    }

    public void Highlight(GameObject obj) {
        if (!gameObject.activeSelf) return;
        highlighted = obj;
        animator.SetBool("Interactive", true);
    }

    public void Blur(GameObject obj) {
        if (!gameObject.activeSelf) return;
        if (highlighted == obj) {
            highlighted = null;
            Reset();
        }
    }

    public void CloseTo(GameObject obj) {
        if (!gameObject.activeSelf) return;
        if (highlighted == obj) animator.SetBool("Near", true);
    }

    public void FarTo(GameObject obj) {
        if (!gameObject.activeSelf) return;
        if (highlighted == obj) animator.SetBool("Near", false);
    }

    public void Teleport(bool status = true) {
        if (!gameObject.activeSelf) return;
        bool blocked = BlockTeleport.blocked;
        animator.SetBool("Denied", status && blocked);
        animator.SetBool("Teleport", status && !blocked);
    }

    public void OnDestroy() {
        if (instance == this) instance = null;
    }
}
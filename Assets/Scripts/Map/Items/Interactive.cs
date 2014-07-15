using UnityEngine;

public class Interactive : MonoBehaviour {
    public delegate void InteractHandler(Interactive instance);
    public event InteractHandler OnInteraction;

    public float farDistance;
    private bool isPlayerClose = false;

    void FixedUpdate() {
        if (PlayerScript.player == null) return;

        isPlayerClose = Vector2.Distance(transform.position, PlayerScript.player.transform.position) <= farDistance;

        if (isPlayerClose) Cursor.instance.CloseTo(gameObject);
        else Cursor.instance.FarTo(gameObject);
    }

    void OnMouseEnter() {
        Cursor.instance.Highlight(gameObject);
    }

    void OnMouseExit() {
        Cursor.instance.Blur(gameObject);
    }

    void OnMouseDown() {
        if (isPlayerClose) {
            if (OnInteraction != null) OnInteraction(this);
        }
    }

    void OnDrawGizmosSelected() {
        // Draw yellow sphere around enemy to highlight range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, farDistance);
    }
}

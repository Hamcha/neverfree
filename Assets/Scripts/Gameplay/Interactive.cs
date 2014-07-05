using UnityEngine;

public class Interactive : MonoBehaviour {
    public float farDistance;
    private bool isPlayerClose = false;

    void FixedUpdate() {
        isPlayerClose = Vector2.Distance(transform.position, PlayerScript.player.transform.position) <= farDistance;
        Debug.Log(isPlayerClose);
        if (isPlayerClose) Cursor.instance.CloseTo(gameObject);
        else               Cursor.instance.FarTo(gameObject);
    }

    void OnMouseEnter() {
        Cursor.instance.Highlight(gameObject);
    }

    void OnMouseExit() {
        Cursor.instance.Blur(gameObject);
    }

    void OnDrawGizmosSelected() {
        // Draw yellow sphere around enemy to highlight range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, farDistance);
    }
}

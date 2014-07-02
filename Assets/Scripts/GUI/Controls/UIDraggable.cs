using UnityEngine;

public class UIDraggable : MonoBehaviour {
    public delegate void DragHandler(UIDraggable instance);
    public event DragHandler DragStarted, DragFinished;

    public bool isDragging { get; private set; }
    public Camera UICamera;
    public bool limitedRange = false;
    public Rect dragRange;

    private Vector3 mouseOffset;

    void OnMouseDrag() {
        if (!isDragging) {
            mouseOffset = GetMousePos() - transform.position;
            isDragging = true;
            if (DragStarted != null) DragStarted(this);
        }

        transform.position = GetMousePos() - mouseOffset;
        if (limitedRange) {
            Vector3 limitedPos = transform.position;
            limitedPos.x = Mathf.Clamp(limitedPos.x, dragRange.xMin, dragRange.xMax);
            limitedPos.y = Mathf.Clamp(limitedPos.y, dragRange.yMin, dragRange.yMax);
            transform.position = limitedPos;
        }
    }

    void OnMouseUp() {
        if (isDragging) {
            isDragging = false;
            if (DragFinished != null) DragFinished(this);
        }
    }

    Vector3 GetMousePos() {
        return UICamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                       Input.mousePosition.y,
                                                       transform.position.z));
    }

    void OnDrawGizmosSelected() {
        if (limitedRange) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(dragRange.center, dragRange.size);
        }
    }
}
using UnityEngine;

public class UIDraggable : MonoBehaviour {
    public delegate void DragHandler(UIDraggable instance);
    public event DragHandler DragStarted, DragFinished;

    public bool isDragging { get; private set; }

    private Vector3 mouseOffset;

    void OnMouseDrag() {
        if (!isDragging) {
            mouseOffset = GetMousePos() - transform.position;
            isDragging = true;
            if (DragStarted != null) DragStarted(this);
        }

        transform.position = GetMousePos() - mouseOffset;
    }

    void OnMouseUp() {
        if (isDragging) {
            isDragging = false;
            if (DragFinished != null) DragFinished(this);
        }
    }

    Vector3 GetMousePos() {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                          Input.mousePosition.y,
                                                          transform.position.z));
    }
}
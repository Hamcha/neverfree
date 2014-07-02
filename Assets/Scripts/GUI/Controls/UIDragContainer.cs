using UnityEngine;

public class UIDragContainer : MonoBehaviour {
    public delegate void DragHandler(UIDragContainer instance);
    public event DragHandler DragStarted, DragFinished;

    public GameObject dragHandle;
    public Camera UICamera;
    public bool isDragging { get; private set; }
    public bool isInside { get; private set; }

    void OnMouseDrag() {
        if (!isDragging) {
            isDragging = true;
            if (DragStarted != null) DragStarted(this);
        }

        if (isInside) {
            Vector3 newpos = GetMousePos();
            newpos.z = transform.position.z;
            dragHandle.transform.position = newpos;
        }
    }

    void OnMouseUp() {
        if (isDragging) {
            isDragging = false;
            if (DragFinished != null) DragFinished(this);
        }
    }

    void OnMouseEnter() {
        isInside = true;
    }

    void OnMouseExit() {
        isInside = false;
        OnMouseUp();
    }

    Vector3 GetMousePos() {
        return UICamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                       Input.mousePosition.y,
                                                       transform.position.z));
    }
}

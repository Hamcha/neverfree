using UnityEngine;

public class UIDraggable : MonoBehaviour {
    public delegate void DragHandler(UIDraggable instance);
    public event DragHandler DragStarted, DragFinished;

    public bool isDragging { get; private set; }
}
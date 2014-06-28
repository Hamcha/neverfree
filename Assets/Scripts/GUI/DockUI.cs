using UnityEngine;

public class DockUI : MonoBehaviour {
    public Vector2 position;
    public Vector2 offset;
    public float pixelSize;

    private Camera UICamera;

    void Start() {
        UICamera = Scene.instance.GUI.UICamera;
    }

    void Update() {
        Vector2 dimensions = CameraTracking.CalculateScreenSizeInWorldCoords(UICamera) / 2;
        transform.position = new Vector3(dimensions.x * position.x + offset.x / pixelSize,
                                         dimensions.y * position.y + offset.y / pixelSize,
                                         UICamera.transform.position.z + 1);
    }
}

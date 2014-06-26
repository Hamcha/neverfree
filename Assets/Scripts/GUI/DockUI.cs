using UnityEngine;
using System.Collections;

public class DockUI : MonoBehaviour {
    public Camera UICamera;
    public Vector2 position;
    public Vector2 offset;
    public float pixelSize;

    void Update() {
        Vector2 dimensions = CameraTracking.CalculateScreenSizeInWorldCoords(UICamera) / 2;
        transform.position = new Vector3(dimensions.x * position.x + offset.x / pixelSize,
                                         dimensions.y * position.y + offset.y / pixelSize,
                                         UICamera.transform.position.z + 1);
    }
}

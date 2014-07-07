using UnityEngine;

public class PixelPerfect : MonoBehaviour {
    public float scale = 2;
    public float pixelScale = 16;
    private float scaleValue { get { return Screen.height / pixelScale / 2.0f / scale; } }

    public void Awake() {
        camera.orthographicSize = scaleValue;
    }

    public void Update() {        
        // Only update if needed
        if (camera.orthographicSize != scaleValue)
            camera.orthographicSize = scaleValue;
    }

    public void Change(float newscale) {
        scale = newscale;
    }
}

using UnityEngine;

public class PixelPerfect : MonoBehaviour {
    public float scale = 2f;
    public float zoomSpeed = 0.5f;
    private float oldScale, targetScale, t;
    public bool zooming = false;
    private float scaleValue = 0;

    void Update() {
        // Are we zooming?
        if (zooming) {
            t += Time.deltaTime * zoomSpeed;
            scale = Mathf.SmoothStep(oldScale, targetScale, t);
            if (t >= 1) zooming = false;
        }

        scaleValue = (Screen.height / 30f / 2.0f / scale);

        // Only update if needed
        if (camera.orthographicSize != scaleValue)
            camera.orthographicSize = scaleValue;
    }

    public void Change(float newscale) {
        oldScale = scale;
        targetScale = newscale;
        t = 0;
        zooming = true;
    }
}

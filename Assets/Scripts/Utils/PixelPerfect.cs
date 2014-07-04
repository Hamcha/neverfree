using UnityEngine;

public class PixelPerfect : MonoBehaviour {
    public float scale = 2;
    public float pixelScale = 16;
    public float zoomSpeed = 0.5f;
    private float oldScale, targetScale, t;
    public bool zooming = false;
    private float scaleValue = 0;

    void Awake() {
        // This is a shameful hack but I don't know better ways to do it ATM
        Update();
    }

    void Update() {
        // Are we zooming?
        if (zooming) {
            t += Time.deltaTime * zoomSpeed;
            scale = Mathf.SmoothStep(oldScale, targetScale, t);
            if (t >= 1) zooming = false;
        }

        scaleValue = (Screen.height / pixelScale / 2.0f / scale);

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

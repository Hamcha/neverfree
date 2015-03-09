using UnityEngine;

public class RangeSprite : MonoBehaviour {
    public Vector3 startScale = new Vector3(0, 0, 1);
    public Vector3 targetScale = Vector3.zero;
    public float targetTime = 1;
    public Vector3 currentScale { get { return transform.localScale; } }

    private Vector3 curVelocity;
    void Update() {
        transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref curVelocity, targetTime);
    }

    public void ScaleTo(float scale, float time) {
        targetScale = new Vector3(scale, scale, 1);
        targetTime = time;
    }

    public void Immediate(float scale) {
        ScaleTo(scale, 0);
    }
}

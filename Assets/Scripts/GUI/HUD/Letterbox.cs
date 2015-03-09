using UnityEngine;

public class Letterbox : MonoBehaviour {
    public float height = 0.1f;
    public float duration = 1f;
    public GameObject top, bottom;
    bool shouldCover = false;
    float currentVelocity;

    void Update() {
        float newHeight = Mathf.SmoothDamp(bottom.transform.localScale.y
                                          , shouldCover ? height : 0
                                          , ref currentVelocity
                                          , duration);
        Vector3 targetVector = new Vector3(1, newHeight, 1);
        top.transform.localScale = bottom.transform.localScale = targetVector;
    }

    public void FadeIn(bool immediate) {
        shouldCover = true;
        if (immediate) {
            top.transform.localScale = bottom.transform.localScale = new Vector3(1, height, 1);
        }
    }

    public void FadeOut() {
        shouldCover = false;
    }
}

using UnityEngine;

public class RandomOffset : MonoBehaviour {
    public float magnitude;
    public float duration;
    public float repeatDelay;

    private float waitDelay;
    private float startTime;

    void Start() {
        waitDelay = 0;
        startTime = 0;
    }

    void Update() {
        // Are we waiting?
        if (waitDelay > 0) {
            waitDelay -= Time.deltaTime;
            if (waitDelay <= 0) startTime = Time.time;
            return;
        }

        if (startTime + duration > Time.time) {
            float x = Random.value * magnitude - magnitude / 2;
            float y = Random.value * magnitude - magnitude / 2;
            renderer.material.SetTextureOffset("_MainTex", new Vector2(x, y));
        } else {
            waitDelay = repeatDelay;
            renderer.material.SetTextureOffset("_MainTex", Vector2.zero);
        }
    }
}

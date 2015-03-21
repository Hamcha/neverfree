using UnityEngine;
using System.Collections;

public class CinematicShotEffect : MonoBehaviour {

    public enum ShotEffectType {
        OddOffset
    }

    public ShotEffectType effect;
    public float strength = 1;
    public float delay = 5;
    public float duration = 0; // 0 = endless

    float startTime;
    bool active = false;

    void Start() {
        StartCoroutine(WaitDelay());
    }

	void Update () {
        if (!active) return;
        if (Time.time - startTime > duration) {
            Debug.Log("Finished shot effect (" + effect.ToString() + ")");
            active = false;
            Restore();
        }

	}

    IEnumerator WaitDelay() {
        yield return new WaitForSeconds(delay);
        Debug.Log("Starting shot effect (" + effect.ToString() + ")");
        Apply();
        startTime = Time.time;
        active = true;
    }

    void Apply() {

    }

    void Restore() {

    }
}

using UnityEngine;
using System.Collections;

public class TrackingTarget : MonoBehaviour {

    public float weight = 10f;

    void Start() {
        Scene.tracker.Add(gameObject, weight);
    }

    void OnDestroy() {
        if (Scene.tracker != null)
            Scene.tracker.Remove(gameObject);
    }
}

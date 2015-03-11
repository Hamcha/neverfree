using UnityEngine;

public class CutsceneTrigger : MonoBehaviour {
    private CutsceneDirector director;

    void Awake() {
        director = GetComponent<CutsceneDirector>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            StartCoroutine(director.Trigger());
        }
    }
}

using UnityEngine;

public class CutsceneTrigger : MonoBehaviour {
    public delegate void CutsceneTriggerHandler(CutsceneTrigger trigger);
    public event CutsceneTriggerHandler CutsceneTriggered;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            if (CutsceneTriggered != null)
                CutsceneTriggered(this);
        }
    }
}

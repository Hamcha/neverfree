using UnityEngine;

public class CutsceneTrigger : MonoBehaviour {
    public delegate void CutsceneTriggerHandler(CutsceneTrigger trigger, bool immediate);
    public event CutsceneTriggerHandler CutsceneTriggered;
    public string cutsceneId;
    public bool immediate = false;
    public bool alwaysDo = false;

    void Awake() {
        if (!alwaysDo && Player.Instance.data.properties.ContainsKey("cutscenes." + cutsceneId)) {
            DestroyImmediate(this);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            if (CutsceneTriggered != null) {
                CutsceneTriggered(this, immediate);
                if (!alwaysDo) Player.Instance.data.properties.Add("cutscenes." + cutsceneId, 1);
            }
        }
    }
}

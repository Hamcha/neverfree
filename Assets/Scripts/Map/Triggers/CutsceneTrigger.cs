using UnityEngine;

public class CutsceneTrigger : MonoBehaviour {
    public delegate void CutsceneTriggerHandler(CutsceneTrigger trigger);
    public event CutsceneTriggerHandler CutsceneTriggered;
    public string cutsceneId;

    void Awake() {
        if (Player.Instance.data.properties.ContainsKey("cutscenes." + cutsceneId)) {
            Debug.Log("OHY");
            Debug.Log(Player.Instance.data.properties["cutscenes." + cutsceneId]);
            DestroyImmediate(this);
        }
            
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            if (CutsceneTriggered != null) {
                CutsceneTriggered(this);
                Player.Instance.data.properties.Add("cutscenes." + cutsceneId, 1);
            }
        }
    }
}

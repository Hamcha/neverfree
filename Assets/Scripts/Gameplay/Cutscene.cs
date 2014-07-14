using UnityEngine;

public class Cutscene : MonoBehaviour {
    CutsceneTrigger trigger;

    void Awake() {
        if (trigger == null)
            trigger = GetComponent<CutsceneTrigger>();
        trigger.CutsceneTriggered += OnTrigger;
    }

    public virtual void OnTrigger(CutsceneTrigger trigger) { }
}

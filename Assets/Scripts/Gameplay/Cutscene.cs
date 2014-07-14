using UnityEngine;

public class Cutscene : MonoBehaviour {
    CutsceneTrigger trigger;

    void Awake() {
        if (trigger == null)
            trigger = GetComponent<CutsceneTrigger>();
        trigger.CutsceneTriggered += (tr) => {
            Scene.gui.StartCutscene();
            OnTrigger(tr);
            Scene.gui.StopCutscene();
        };
    }

    public virtual void OnTrigger(CutsceneTrigger trigger) { }
}

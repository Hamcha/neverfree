using System.Collections;
using UnityEngine;

public class Cutscene : MonoBehaviour {
    public delegate void CutsceneFinishedHandler(Cutscene cutscene);
    public event CutsceneFinishedHandler CutsceneFinished;
    CutsceneTrigger trigger;

    void Awake() {
        if (trigger == null) trigger = GetComponent<CutsceneTrigger>();

        trigger.CutsceneTriggered += (tr, im) => StartCoroutine(OnTriggerWrapper(im));
    }

    public virtual void DoCutscene() { }

    protected void Terminate() {
        if (CutsceneFinished != null)
            CutsceneFinished(this);
    }

    public IEnumerator OnTriggerWrapper(bool immediate) {
        Scene.gui.StartCutscene(immediate);
        if (!immediate) yield return new WaitForSeconds(Scene.gui.letterbox.duration);
        CutsceneFinished += (cut) => {
            Scene.gui.StopCutscene();
            gameObject.SetActive(false);
        };
        DoCutscene();

    }
}

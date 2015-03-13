using System.Collections;
using UnityEngine;

public class CutsceneShowObject : CutsceneEvent {
    public GameObject focusOn;
    public float duration;

    public override IEnumerator DoCutscene() {
        if (focusOn != null) {
            Scene.gui.tracker.Add(focusOn, 10000);
        }
        yield return new WaitForSeconds(duration);
        if (focusOn != null) {
            Scene.gui.tracker.Remove(focusOn);
        }
    }
}

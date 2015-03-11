using System.Collections;
using UnityEngine;

public class ShowObject : CutsceneEvent {
    public GameObject focusOn;
    public float duration;

    public override IEnumerator DoCutscene() {
        Scene.gui.tracker.Add(focusOn, 10000);
        yield return new WaitForSeconds(duration);
        Scene.gui.tracker.Remove(focusOn);
    }
}

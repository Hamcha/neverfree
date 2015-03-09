using System.Collections;
using UnityEngine;

public class ShowObject : Cutscene {
    public GameObject focusOn;
    public float duration;

    public override void DoCutscene() {
        StartCoroutine(ShowObjectCutscene());
    }

    IEnumerator ShowObjectCutscene() {
        Scene.gui.tracker.Add(focusOn, 10000);
        yield return new WaitForSeconds(duration);
        Scene.gui.tracker.Remove(focusOn);
        Terminate();
    }
}

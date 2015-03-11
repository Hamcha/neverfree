using System.Collections;
using UnityEngine;

class IdleCutscene : CutsceneEvent {
    public float duration = 3;

    public override IEnumerator DoCutscene() {
        yield return new WaitForSeconds(duration);
    }
}

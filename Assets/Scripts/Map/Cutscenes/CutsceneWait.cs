using System.Collections;
using UnityEngine;

class CutsceneWait : CutsceneEvent {
    public float duration = 3;

    public override IEnumerator DoCutscene() {
        yield return new WaitForSeconds(duration);
    }
}

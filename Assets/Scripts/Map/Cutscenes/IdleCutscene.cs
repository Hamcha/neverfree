using System.Collections;
using UnityEngine;

class IdleCutscene : Cutscene {
    public float duration;
    public override void DoCutscene() {
        StartCoroutine(DoIdleCutscene());
    }

    IEnumerator DoIdleCutscene() {
        yield return new WaitForSeconds(duration);
        Terminate();
    }
}

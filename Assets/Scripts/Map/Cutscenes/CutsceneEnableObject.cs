using System.Collections;
using UnityEngine;

class CutsceneEnableObject : CutsceneEvent {
    public GameObject target = null;

    public override IEnumerator DoCutscene() {
        target.SetActive(true);
        yield break;
    }
}
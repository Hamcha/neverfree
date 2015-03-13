using System.Collections;
using UnityEngine;

class EnableObject : CutsceneEvent {
    public GameObject target = null;

    public override IEnumerator DoCutscene() {
        target.SetActive(true);
        yield break;
    }
}
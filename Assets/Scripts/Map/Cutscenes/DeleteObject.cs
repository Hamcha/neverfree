using System.Collections;
using UnityEngine;
class DeleteObject : CutsceneEvent {
    public GameObject toDelete;

    public override IEnumerator DoCutscene() {
        if (toDelete == null) {
            Debug.LogError("Target object to delete is inexistent");
        } else {
            Destroy(toDelete);
        }
        yield break;
    }
}
using System.Collections;
using UnityEngine;

class CutsceneSpawnObject : CutsceneEvent {
    public GameObject template = null;
    public Vector3 position = Vector3.zero;
    public Quaternion rotation = Quaternion.identity;

    public override IEnumerator DoCutscene() {
        Instantiate(template, position, rotation);
        yield break;
    }
}

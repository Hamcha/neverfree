using System.Collections;
using UnityEngine;

class CutsceneTurnPlayer : CutsceneEvent {
    public bool useConstantDirection = false;
    public float direction = 0;
    public bool lookAtObject = true;
    public Transform targetObject = null;

    public override IEnumerator DoCutscene() {
        if (useConstantDirection) {
            PlayerScript.instance.Turn(direction);
        }
        if (lookAtObject) {
            float dif = targetObject.position.x - PlayerScript.instance.gameObject.transform.position.x;
            PlayerScript.instance.Turn(dif);
        }
        yield break;
    }
}

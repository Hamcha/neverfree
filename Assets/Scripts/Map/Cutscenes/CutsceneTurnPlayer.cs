using System.Collections;
using UnityEngine;

class CutsceneTurnPlayer : CutsceneEvent {
    public bool useConstantDirection;
    public float direction;
    public bool lookAtObject;
    public Transform targetObject;

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

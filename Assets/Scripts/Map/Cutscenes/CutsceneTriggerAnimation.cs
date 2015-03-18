using System.Collections;
using UnityEngine;

class CutsceneTriggerAnimation : CutsceneEvent {
    public GameObject target = null;
    public string animationTrigger = "";

    public override IEnumerator DoCutscene() {
        target.GetComponent<Animator>().SetTrigger(animationTrigger);
        yield break;
    }
}

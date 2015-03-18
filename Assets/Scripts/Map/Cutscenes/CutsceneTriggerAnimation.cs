using System.Collections;
using UnityEngine;

class CutsceneTriggerAnimation : CutsceneEvent {
    public GameObject target = null;
    public string animationTrigger = "";
    public float delay = 0;

    public override IEnumerator DoCutscene() {
        yield return new WaitForSeconds(delay);
        target.GetComponent<Animator>().SetTrigger(animationTrigger);
        yield break;
    }
}

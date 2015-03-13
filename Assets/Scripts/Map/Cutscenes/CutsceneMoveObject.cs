using System.Collections;
using UnityEngine;

public class CutsceneMoveObject : CutsceneEvent {
    public GameObject toMove;
    public Vector3 destination;
    public float duration;
    public float delay = 0;
    public bool triggerAnimationStatus;
    public string animationTrigger;

    private bool active = false;
    private float startTime = 0;
    private Vector3 originalPosition;
    const float threshold = 0.1f;

    void Update() {
        if (!active) return;
        if (Time.time > startTime + duration) active = false;

        toMove.transform.position = Vector3.Lerp(originalPosition, destination, (Time.time - startTime) / duration);
    }

    public override IEnumerator DoCutscene() {
        StartCoroutine(DelayedStart());
        yield break;
    }

    public IEnumerator DelayedStart() {
        yield return new WaitForSeconds(delay);
        active = true;
        startTime = Time.time;
        originalPosition = toMove.transform.position;
        if (triggerAnimationStatus) {
            toMove.GetComponent<Animator>().SetTrigger(animationTrigger);
        }
    }
}

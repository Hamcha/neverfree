using System.Collections;
using UnityEngine;

public class CutsceneEvent : MonoBehaviour {
    protected CutsceneDirector director;
    public int order;

    void Awake() {
        director = GetComponent<CutsceneDirector>();
        if (director == null) Debug.LogError("Cutscene director not found!");
        director.Add(order, DoCutscene);
    }

    public virtual IEnumerator DoCutscene() { yield break; }
}

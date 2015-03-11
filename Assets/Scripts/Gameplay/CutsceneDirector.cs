using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDirector : MonoBehaviour {
    public string cutsceneId;
    public bool immediate = false;
    public bool alwaysDo = false;

    private List<Func<IEnumerator>> cutsceneEvents = new List<Func<IEnumerator>>();

    void Awake() {
        if (!alwaysDo && Player.Instance.data.properties.ContainsKey("cutscenes." + cutsceneId)) {
            DestroyImmediate(this);
        }
    }

    public IEnumerator Trigger() {
        // Start cutscene (immediate puts letterbox on instantly)
        Scene.gui.StartCutscene(immediate);
        if (!immediate) yield return new WaitForSeconds(Scene.gui.letterbox.duration);
        
        // Cycle through all registered events
        foreach (Func<IEnumerator> cutEvent in cutsceneEvents) {
            yield return StartCoroutine(cutEvent());
        }

        // Register cutscene as done unless specified
        if (!alwaysDo) Player.Instance.data.properties.Add("cutscenes." + cutsceneId, 1);

        // Stop and disable cutscene
        Scene.gui.StopCutscene();
        gameObject.SetActive(false);
    }

    internal void Add(int order, Func<IEnumerator> cutsceneEvent) {
        cutsceneEvents.Insert(order, cutsceneEvent);
    }
}

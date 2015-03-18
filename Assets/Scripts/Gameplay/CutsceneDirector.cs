using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDirector : MonoBehaviour {
    public string cutsceneId;
    public bool immediate = false;
    public bool alwaysDo = false;

    private Dictionary<int, Func<IEnumerator>> cutsceneEvents = new Dictionary<int, Func<IEnumerator>>();
    private List<int> eventIds = new List<int>();

    void Awake() {
        if (!alwaysDo && Player.Instance.data.properties.ContainsKey("cutscenes." + cutsceneId)) {
            DestroyImmediate(gameObject);
        }
    }

    public IEnumerator Trigger() {
        // Start cutscene (immediate puts letterbox on instantly)
        Scene.gui.StartCutscene(immediate);
        if (!immediate) yield return new WaitForSeconds(Scene.gui.letterbox.duration);
        
        // Cycle through all registered events
        eventIds.Sort();
        foreach (int eventId in eventIds) {
            Debug.Log("[" + cutsceneId + "] Playing #" + eventId + " (" + cutsceneEvents[eventId].Target + ")");
            yield return StartCoroutine(cutsceneEvents[eventId]());
        }

        // Register cutscene as done unless specified
        if (!alwaysDo) Player.Instance.data.properties.Add("cutscenes." + cutsceneId, 1);

        // Stop and disable cutscene
        Scene.gui.StopCutscene();
        gameObject.SetActive(false);
    }

    public void Add(int order, Func<IEnumerator> cutsceneEvent) {
        eventIds.Add(order);
        cutsceneEvents.Add(order, cutsceneEvent);
    }
}

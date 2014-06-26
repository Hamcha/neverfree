using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {
    public static Scene instance { get; private set; }

    public GameObject player;
    public GameObject GUIPrefab;

    public GUIScript GUI;

    void Awake() {
        GUI = FindObjectOfType<GUIScript>();
        // Not found? Create one!
        if (GUI == null) {
            GUI = ((GameObject)Instantiate(GUIPrefab)).GetComponent<GUIScript>();
        }
        
        // Setup tracking camera
        GUI.tracker.Clear();
        GUI.tracker.Add(player, 100f);

        instance = this;
    }

    void OnDestroy() {
        if (instance == this) instance = null;
    }
}

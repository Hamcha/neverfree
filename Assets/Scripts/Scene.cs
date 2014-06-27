using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {
    public static Scene instance { get; private set; }

    public GameObject GUIPrefab;
    public GameObject playerPrefab;

    public GameObject player;
    public GameObject terrain;

    public GUIScript GUI;

    public static GUIScript gui { get { return instance.GUI; } }
    public static CameraTracking tracker { get { return gui.tracker; } }

    void Awake() {
        // Create and setup Player
        player = (GameObject)Instantiate(playerPrefab);

        // Create and setup GUI if needed
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

using UnityEngine;

public class Scene : MonoBehaviour {
    public static Scene instance { get; private set; }

    public GameObject GUIPrefab;
    public GameObject playerPrefab;
    public GameObject storagePrefab;

    public GameObject player;
    public GameObject terrain;

    public GUIScript GUI;
    public Prefabs storage;
    public static GUIScript gui { get { return instance != null ? instance.GUI : null; } }
    public static CameraTracking tracker { get { return gui != null ? gui.tracker : null; } }

    void Awake() {
        // Create and setup Storage
        storage = ((GameObject)Instantiate(storagePrefab)).GetComponent<Prefabs>();

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

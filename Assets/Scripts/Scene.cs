using UnityEngine;

public class Scene : MonoBehaviour {
    public static Scene instance { get; private set; }

    public string zoneName;

    public GameObject GUIPrefab;
    public GameObject playerPrefab;
    public GameObject storagePrefab;

    public GameObject player;
    public GameObject terrain;
    public GameObject borders;

    public GUIScript GUI;
    public Prefabs storage;
    public static GUIScript gui { get { return instance != null ? instance.GUI : null; } }
    public static CameraTracking tracker { get { return gui != null ? gui.tracker : null; } }

    void Awake() {
        // Create and setup Storage if needed
        storage = Prefabs.instance == null ? ((GameObject)Instantiate(storagePrefab)).GetComponent<Prefabs>()
                                           : Prefabs.instance;

        // Create and setup Player if needed
        player = PlayerScript.player == null ? (GameObject)Instantiate(playerPrefab)
                                             : PlayerScript.player;

        // Position player
        if (string.IsNullOrEmpty(Player.Instance.transitionBorder)) {
            Player.Instance.transitionBorder = borders.transform.GetChild(0).name;
        }
        player.transform.position = GetBorder(Player.Instance.transitionBorder).position;

        // Create and setup GUI if needed
        GUI = GUIScript.instance == null ? ((GameObject)Instantiate(GUIPrefab)).GetComponent<GUIScript>()
                                         : GUIScript.instance;

        // Setup tracking camera
        GUI.tracker.terrain = terrain.GetComponent<SpriteRenderer>();
        GUI.tracker.Clear();
        GUI.tracker.Add(player, 100f);
        GUI.tracker.InstantMove();

        // Setup zone name
        GUI.zoneName.text = zoneName;
        GUI.zoneName.animation.Play();

        instance = this;
    }

    void OnDestroy() {
        GUI.tracker.terrain = null;
        if (instance == this) instance = null;
    }

    public Transform GetBorder(string name) {
        return borders.transform.Find(name);
    }
}

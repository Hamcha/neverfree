using UnityEngine;

public class GUIScript : MonoBehaviour {
    public HealthBar healthBar;
    public Camera UICamera;
    public CameraTracking tracker;
    public StanceBar stanceBar;
    public Cursor cursor;
    public GUIText zoneName;
    public static GUIScript instance;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    void OnDestroy() {
        if (instance == this) instance = null;
    }
}

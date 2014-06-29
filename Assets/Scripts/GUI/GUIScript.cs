using UnityEngine;

public class GUIScript : MonoBehaviour {
    public HealthBar healthBar;
    public Camera UICamera;
    public CameraTracking tracker;
    public StanceBar stanceBar;
    public Cursor cursor;
    public GUIText zoneName;

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}

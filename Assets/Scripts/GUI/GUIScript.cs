using UnityEngine;

public class GUIScript : MonoBehaviour {
    public HealthBar healthBar;
    public Camera UICamera;
    public CameraTracking tracker;
    public StanceBar stanceBar;
    public Cursor cursor;
    public GUIText zoneName;
    public Letterbox letterbox;
    public Transition transition;
    public static GUIScript instance;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    void OnDestroy() {
        if (instance == this) instance = null;
    }

    public void StartCutscene() {
        letterbox.FadeIn();
        // Stop player
        PlayerScript.instance.baseAnimator.SetFloat("Speed", 0);
        cursor.gameObject.SetActive(false);
        PlayerScript.instance.disabled = true;
    }

    public void StopCutscene() {
        letterbox.FadeOut();
        cursor.gameObject.SetActive(true);
        PlayerScript.instance.disabled = false;
    }
}

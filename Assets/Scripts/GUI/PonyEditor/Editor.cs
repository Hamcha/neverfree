using UnityEngine;

public class Editor : MonoBehaviour {
    public static Editor instance { get; private set; }

    public GameObject storagePrefab;

    public SpriteRenderer playerBase, playerMane;

    public int maneStyle {
        get { return Player.Instance.data.maneStyle; }
        set {
            Player.Instance.data.maneStyle = value;
            UpdateCharacter();
        }
    }

    /*public Color bodyColor {
        get { }
    }*/

    public Prefabs storage;

    void Awake() {
        // Create and setup Storage if needed
        storage = Prefabs.instance == null ? ((GameObject)Instantiate(storagePrefab)).GetComponent<Prefabs>()
                                           : Prefabs.instance;

        instance = this;
    }

    void UpdateCharacter() {
        playerMane.sprite = storage.maneStyles[maneStyle];
    }

    void OnDestroy() {
        if (instance == this) instance = null;
    }
}

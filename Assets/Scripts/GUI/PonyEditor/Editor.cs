using UnityEngine;

public class Editor : MonoBehaviour {
    public static Editor instance { get; private set; }

    public GameObject storagePrefab;

    public SpriteRenderer playerBase, playerMane;

    public int hairStyle {
        get { return Player.Instance.data.hairStyle; }
        set {
            Player.Instance.data.hairStyle = value;
            UpdateCharacter();
        }
    }

    public Prefabs storage;

    void Awake() {
        // Create and setup Storage if needed
        storage = Prefabs.instance == null ? ((GameObject)Instantiate(storagePrefab)).GetComponent<Prefabs>()
                                           : Prefabs.instance;

        instance = this;
    }

    void UpdateCharacter() {
        playerMane.sprite = storage.hairStyles[hairStyle];
    }

    void OnDestroy() {
        if (instance == this) instance = null;
    }
}

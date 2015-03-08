using System.Collections;
using UnityEngine;

public class PonyEditor : MonoBehaviour {
    public delegate void ColorChangedHandler(Color bodyColor, Color maneColor);
    public event ColorChangedHandler ColorChanged;

    public static PonyEditor instance { get; private set; }

    public GameObject storagePrefab;

    public SpriteRenderer playerBase, playerMane;
    public ColorPicker bodySelector, maneSelector;

    const bool devMode = false; // Make true to turn into a char editor for saved games

    public int maneStyle {
        get { return Player.Instance.data.maneStyle; }
        set {
            Player.Instance.data.maneStyle = value;
            UpdateCharacter();
        }
    }

    public Color bodyColor {
        get { return Player.Instance.data.bodyColor; }
        set {
            Player.Instance.data.bodyColor = value;
            UpdateCharacter();
            if (ColorChanged != null) ColorChanged(value, maneColor);
        }
    }
    public Color maneColor {
        get { return Player.Instance.data.maneColor; }
        set {
            Player.Instance.data.maneColor = value;
            UpdateCharacter();
            if (ColorChanged != null) ColorChanged(bodyColor, value);
        }
    }

    public Prefabs storage;

    void Awake() {
        // Create and setup Storage if needed
        storage = Prefabs.instance == null ? ((GameObject)Instantiate(storagePrefab)).GetComponent<Prefabs>()
                                           : Prefabs.instance;

        instance = this;

        bodySelector.ColorChanged += (_, color) => bodyColor = color;
        maneSelector.ColorChanged += (_, color) => maneColor = color;
        if (Application.isEditor && devMode) {
            Player.Instance.Load();
        }
        UpdateCharacter();
    }

    void UpdateCharacter() {
        playerMane.sprite = storage.maneStyles[maneStyle];
        playerMane.color = maneColor;
        playerBase.color = bodyColor;
        if (Application.isEditor && devMode) {
            Player.Instance.Save();
        }
    }

    void OnDestroy() {
        if (instance == this) instance = null;
    }

    public void StartGame() {
        StartCoroutine(StartGameAsync());
    }

    private IEnumerator StartGameAsync() {
        yield return Application.LoadLevelAdditiveAsync("Loading screen");
        Application.LoadLevelAsync("Starting room");
    }
}

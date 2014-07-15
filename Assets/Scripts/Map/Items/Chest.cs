using UnityEngine;

public class Chest : MonoBehaviour {
    public bool randomContent = true;
    private bool opened = false;

    void Awake() {
        GetComponent<Interactive>().OnInteraction += TryOpen;
    }

    void TryOpen(Interactive source) {
        if (opened) return;

        opened = true;
    }
}

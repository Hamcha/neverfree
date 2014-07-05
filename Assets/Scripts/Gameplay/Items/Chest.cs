using UnityEngine;

public class Chest : MonoBehaviour {
    void Awake() {
        GetComponent<Interactive>().OnInteraction += TryOpen;
    }

    void TryOpen(Interactive source) {
        Debug.Log("Opening chest!");
    }
}

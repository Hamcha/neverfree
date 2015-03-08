using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

    public GUIText[] items;
    public float navigationDelay = 0.5f;
    private float delay = 0;
    private int selected = 0;

    void Start () {
        if (!Player.Instance.SaveExists()) {
            Destroy(items[1]); // Destroy continue option if save doesn't exist
        }
        foreach (GUIText item in items) {
            item.color = new Color(1, 1, 1, 0.3f);
        }
        Select(0);
    }

    void Update() {
        if (delay > 0) {
            delay -= Time.deltaTime;
            return;
        }
        float movement = Input.GetAxis("Vertical");
        if (movement != 0) {
            if (movement < 0 && selected < items.Length - 1) {
                Select(selected + 1);
                delay = navigationDelay;
            }
            if (movement > 0 && selected > 0) {
                Select(selected - 1);
                delay = navigationDelay;
            }
        }
    }

    private void Select(int id) {
        items[selected].color = new Color(1, 1, 1, 0.3f);
        items[id].color = new Color(1, 1, 1, 0.8f);
        selected = id;
    }
}

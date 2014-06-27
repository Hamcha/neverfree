using UnityEngine;
using System.Collections.Generic;

public class StanceBar : MonoBehaviour {
    public GameObject Stance;
    public Camera UICamera;
    public GameObject Stance_SelectBox;
    private List<GameObject> stances;

    // Get current stance
    private int _selected;
    public int selected {
        get { return _selected; }
        set {
            _selected = value;
            Player.Instance.stance = Player.Instance.data.stances[_selected];
        }
    }

    void Start() {
        // Create local stance list
        stances = new List<GameObject>();

        // Create stance UI objects
        for (int i = 0; i < Player.Instance.data.stances.Count; i++) {
            GameObject stance = (GameObject)Instantiate(Stance);
            stance.transform.parent = transform;
            DockUI dock = stance.GetComponent<DockUI>();
            dock.offset.y = 21;
            stance.GetComponent<Animator>().SetTrigger(Player.Instance.data.stances[i].ToString());
            stances.Add(stance);
        }

        // Raise selected stance
        Raise(0);
    }

    void Raise(int id) {
        // Put SelectBox over stance
        DockUI dock = Stance_SelectBox.GetComponent<DockUI>();
        dock.offset.x = 6 + 11 * id;
        dock.offset.y = 21;

        // Move stances to make space for SelectBox
        int offset = 4;
        for (int i = 0; i < stances.Count; i++) {
            if (i == id) offset += 2;
            stances[i].GetComponent<DockUI>().offset.x = offset;
            if (i == id) offset += 2;
            offset += 11;
        }

        // Make cursor check for changes
        Cursor.setCursorType(Player.Instance.data.stances[id].ToString());

        selected = id;
    }

    public void Next() {
        // Is the last one already selected?
        if (selected + 1 >= stances.Count) return;

        Raise(selected + 1);
    }

    public void Back() {
        // Is the first one already selected?
        if (selected - 1 < 0) return;

        Raise(selected - 1);
    }
}

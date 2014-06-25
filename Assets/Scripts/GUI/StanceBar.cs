using UnityEngine;
using System.Collections.Generic;

public class StanceBar : MonoBehaviour {
    public GameObject Stance;
    public Camera UICamera;
    public GameObject Stance_SelectBox;
    private List<GameObject> stances;

    // Get current stance as int
    public int selected {
        get { return (int)Player.Instance.stance; }
        set { Player.Instance.stance = (Player.Stance)value; }
    }

    public Player.Stance[] stanceList = new Player.Stance[] { Player.Stance.STANCE_VIEW, Player.Stance.STANCE_BASE_SHOT };

    void Start() {
        // Create local stance list
        stances = new List<GameObject>();

        // Create stance UI objects
        for (int i = 0; i < Player.Instance.data.stances.Length; i++) {
            GameObject stance = (GameObject)Instantiate(Stance);
            stance.transform.parent = transform;
            DockUI dock = stance.GetComponent<DockUI>();
            dock.UICamera = UICamera;
            dock.offset.x = 5 + 13 * i;
            dock.offset.y = 21;
            stance.GetComponent<Animator>().SetTrigger(Player.Instance.data.stances[i]);
            stances.Add(stance);
        }

        // Raise selected stance
        Raise((int)Player.Instance.stance);
    }

    void Raise(int id) {
        // Put SelectBox over stance
        DockUI dock = Stance_SelectBox.GetComponent<DockUI>();
        dock.offset.x = 5 + 13 * id;
        dock.offset.y = 21;

        // Make cursor check for changes
        Cursor.setCursorType(Player.Instance.data.stances[id]);

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

using UnityEngine;
using System.Collections.Generic;

public class AbilityBar : MonoBehaviour {
    public GameObject Ability;
    public Camera UICamera;
    public GameObject Ability_SelectBox;
    public List<GameObject> abilities;

    // Get current ability
    private int _selected;
    public int selected {
        get { return _selected; }
        set {
            _selected = value;
            Player.Instance.ability = Player.Instance.data.abilities[_selected];
        }
    }

    void Start() {
        // Create local ability list
        abilities = new List<GameObject>();

        // Create ability UI objects
        for (int i = 0; i < Player.Instance.data.abilities.Count; i++) {
            GameObject ability = (GameObject)Instantiate(Ability);
            ability.transform.parent = transform;
            DockUI dock = ability.GetComponent<DockUI>();
            dock.offset.y = 15;
            ability.GetComponent<Animator>().SetTrigger(Player.Instance.data.abilities[i].ToString());
            abilities.Add(ability);
        }

        // Raise selected ability
        Raise(0);
    }

    public void Raise(int id) {
        // Put SelectBox over ability
        DockUI dock = Ability_SelectBox.GetComponent<DockUI>();
        dock.offset.x = 7 + 11 * id;
        dock.offset.y = 15;

        // Move abilities to make space for SelectBox
        int offset = 5;
        for (int i = 0; i < abilities.Count; i++) {
            if (i == id) offset += 2;
            abilities[i].GetComponent<DockUI>().offset.x = offset;
            if (i == id) offset += 2;
            offset += 11;
        }

        // Make cursor check for changes
        Cursor.instance.SetCursorAbility(Player.Instance.data.abilities[id].ToString());

        selected = id;
    }

    public void Next() {
        // Is the last one already selected?
        if (selected + 1 >= abilities.Count) return;

        Raise(selected + 1);
    }

    public void Back() {
        // Is the first one already selected?
        if (selected - 1 < 0) return;

        Raise(selected - 1);
    }
}

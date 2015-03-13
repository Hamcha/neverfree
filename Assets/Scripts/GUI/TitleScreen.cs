using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TitleScreen : MonoBehaviour {

    public GameObject[] items;
    private bool idle = true;

    // Items
    // 0 - Start
    // 1 - Continue

    void Start () {
        if (!Player.Instance.SaveExists()) {
            Destroy(items[1]); // Destroy continue option if save doesn't exist
        }
    }

    public void Accept(int option) {
        if (!idle) return;
        idle = false;
        switch (option) {
            case 0: // Start new game
                StartCoroutine(NewGame());
                break;
            case 1: // Load saved game
                StartCoroutine(ResumeGame());
                break;
        }
    }
    private IEnumerator NewGame() {
        PlayerData data = new PlayerData();
        data.hearts = 3;
        data.maneStyle = 0;
        data.bodyColor = new Color(1, 1, 1);
        data.maneColor = new Color(.8f, .4f, .3f);
        data.abilities = new List<Player.Ability>() { Player.Ability.Inspect };
        data.properties = new SaveTable<object>();
        Player.Instance.data = data;
        Player.Instance.health = data.hearts * 2;
        yield return Application.LoadLevelAdditiveAsync("Loading screen");
        Application.LoadLevelAsync("Character editor");
    }

    private IEnumerator ResumeGame() {
        Player.Instance.Load();
        yield return Application.LoadLevelAdditiveAsync("Loading screen");
        Application.LoadLevelAsync("Save room");
    }
}

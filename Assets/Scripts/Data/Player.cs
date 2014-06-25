using UnityEngine;
public class Player : Singleton<Player> {
    public enum Stance {
        STANCE_VIEW = 0,
        STANCE_BASE_SHOT = 1
    };

    protected Player() { }

    public int health = 0;
    public Stance stance = Stance.STANCE_VIEW;
    public PlayerData data = new PlayerData();

    void Awake() {
        data.Load();
        health = data.hearts;
    }
}

[System.Serializable]
public class PlayerData {
    public string name;
    public int hearts;
    public string[] stances;

    public void Save() {
        PlayerPrefs.SetString("playerName", name);
        PlayerPrefs.SetInt("playerHearts", hearts);
        PlayerPrefs.SetString("playerStances", string.Join(",", stances));
        PlayerPrefs.Save();
    }

    public void Load() {/*
        name    = PlayerPrefs.GetString("playerName");
        hearts  = PlayerPrefs.GetInt("playerHearts");
        stances = PlayerPrefs.GetString("playerStances").Split(',');
      */
        name = "Unnamed pony";
        hearts = 3;
        stances = new string[] { "look", "ray" };
    }
}
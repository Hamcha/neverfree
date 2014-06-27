using UnityEngine;
public class Player : Singleton<Player> {

    public delegate void HealthChangedHandler(Player instance, int newHealth);
    public event HealthChangedHandler HealthChanged;

    public delegate void DeathHandler(Player instance);
    public event DeathHandler Died;

    public enum Stance {
        Inspect = 0,
        BaseShot = 1
    };

    protected Player() { }


    private int _health;
    public int health {
        get { return _health; }
        set {
            _health = value;
            if (_health <= 0) {
                _health = 0;
                if (Died != null) Died(this);
            }
            if (HealthChanged != null) HealthChanged(this, _health);
        }
    }
    public Stance stance = Stance.Inspect;
    public PlayerData data = new PlayerData();

    void Awake() {
        data.Load();
        health = data.hearts * 2;
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
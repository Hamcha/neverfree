using System;
using System.Collections.Generic;
using UnityEngine;
public class Player : Singleton<Player> {

    public delegate void HealthChangedHandler(Player instance, int newHealth);
    public event HealthChangedHandler HealthChanged;

    public delegate void DeathHandler(Player instance);
    public event DeathHandler Died;

    public enum Stance {
        Inspect, BaseShot
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

public class PlayerData {
    public string name;
    public int hearts;
    public List<Player.Stance> stances;

    public void Save() {
        PlayerPrefs.SetString("playerName", name);
        PlayerPrefs.SetInt("playerHearts", hearts);
        string[] stancesStr = stances.ConvertAll((x) => x.ToString()).ToArray();
        PlayerPrefs.SetString("playerStances", string.Join(",", stancesStr));
        PlayerPrefs.Save();
    }

    public void Load() {
        name = Get("playerName", "Unnamed pony");
        hearts = Get("playerHearts", 5);
        string[] stancesStr = Get("playerStances", "Inspect,BaseShot").Split(',');
        stances = new List<string>(stancesStr).ConvertAll((x) => (Player.Stance)Enum.Parse(typeof(Player.Stance), x));
    }

    private string Get(string key, string fallback) {
        return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : fallback;
    }

    private int Get(string key, int fallback) {
        return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : fallback;
    }

    private float Get(string key, float fallback) {
        return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : fallback;
    }
}
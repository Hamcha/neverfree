using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player> {
    #region Events
    public delegate void HealthChangedHandler(Player instance, int newHealth);
    public event HealthChangedHandler HealthChanged;

    public delegate void DeathHandler(Player instance);
    public event DeathHandler Died;
    #endregion

    #region Enums
    public enum Stance {
        Inspect, BaseShot
    };
    #endregion

    #region Properties
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
    public string transitionBorder = string.Empty;
    public string lastMap = string.Empty;
    #endregion

    #region Unity callbacks
    void Awake() {
        data.Load();
        health = data.hearts * 2;
    }
    #endregion
}

public class PlayerData {
    #region Saved properties
    public string name;
    public int hearts;
    public int maneStyle;
    public Color bodyColor, maneColor;
    public List<Player.Stance> stances;
    public float teleportRange;
    #endregion

    #region Save/Load
    public void Save() {
        PlayerPrefs.SetString("playerName", name);
        PlayerPrefs.SetInt("playerHearts", hearts);
        string[] stancesStr = stances.ConvertAll((x) => x.ToString()).ToArray();
        PlayerPrefs.SetString("playerStances", string.Join(",", stancesStr));
        PlayerPrefs.SetInt("playerManeStyle", maneStyle);
        PlayerPrefs.SetString("playerBodyColor", bodyColor.r + "," + bodyColor.g + "," + bodyColor.b);
        PlayerPrefs.SetString("playerManeColor", maneColor.r + "," + maneColor.g + "," + maneColor.b);
        PlayerPrefs.SetFloat("playerTeleportRange", teleportRange);
        PlayerPrefs.Save();
    }

    public void Load() {
        name = Get("playerName", "Unnamed pony");
        hearts = Get("playerHearts", 5);
        maneStyle = Get("playerManeStyle", 0);
        string[] stancesStr = Get("playerStances", "Inspect,BaseShot").Split(',');
        stances = new List<string>(stancesStr).ConvertAll((x) => (Player.Stance)Enum.Parse(typeof(Player.Stance), x));
        List<float> bodyColorVals = new List<string>(Get("playerBodyColor", "1,1,1").Split(',')).ConvertAll((x) => float.Parse(x));
        bodyColor = new Color(bodyColorVals[0], bodyColorVals[1], bodyColorVals[2]);
        List<float> maneColorVals = new List<string>(Get("playerManeColor", "0.8,0.4,0.3").Split(',')).ConvertAll((x) => float.Parse(x));
        maneColor = new Color(maneColorVals[0], maneColorVals[1], maneColorVals[2]);
        teleportRange = Get("playerTeleportRange", 5f);
    }
    #endregion

    #region Utils
    private string Get(string key, string fallback) {
        return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : fallback;
    }

    private int Get(string key, int fallback) {
        return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : fallback;
    }

    private float Get(string key, float fallback) {
        return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : fallback;
    }
    #endregion
}
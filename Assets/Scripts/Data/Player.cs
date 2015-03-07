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
    public Dictionary<string, string> data;
    public float teleportRange;
    static Dictionary<string, string> DefaultProperties = new Dictionary<string, string> {
        {"tutorialStage","0"}
    };
    #endregion

    #region Save/Load
    public void Save() {
        // Save basic player properties
        PlayerPrefs.SetString("player.Name", name);
        PlayerPrefs.SetInt("player.Hearts", hearts);
        PlayerPrefs.SetFloat("player.TeleportRange", teleportRange);
        string[] stancesStr = stances.ConvertAll((x) => x.ToString()).ToArray();
        PlayerPrefs.SetString("player.Stances", string.Join(",", stancesStr));

        // Save player style
        PlayerPrefs.SetInt("player.ManeStyle", maneStyle);
        PlayerPrefs.SetString("player.BodyColor", bodyColor.r + "," + bodyColor.g + "," + bodyColor.b);
        PlayerPrefs.SetString("player.ManeColor", maneColor.r + "," + maneColor.g + "," + maneColor.b);

        // Save game properties/triggers
        foreach (KeyValuePair<string, string> property in data) {
            PlayerPrefs.SetString("player.data." + property.Key, property.Value);
        }

        // Save to disk
        PlayerPrefs.Save();
    }

    public void Load() {
        // Load basic player properties
        name = Get("player.Name", "Unnamed pony");
        hearts = Get("player.Hearts", 3);
        teleportRange = Get("player.TeleportRange", 0);
        string[] stancesStr = Get("player.Stances", "Inspect").Split(',');
        stances = new List<string>(stancesStr).ConvertAll((x) => (Player.Stance)Enum.Parse(typeof(Player.Stance), x));

        // Load player style
        maneStyle = Get("player.ManeStyle", 0);
        List<float> bodyColorVals = new List<string>(Get("player.BodyColor", "1,1,1").Split(',')).ConvertAll((x) => float.Parse(x));
        bodyColor = new Color(bodyColorVals[0], bodyColorVals[1], bodyColorVals[2]);
        List<float> maneColorVals = new List<string>(Get("player.ManeColor", "0.8,0.4,0.3").Split(',')).ConvertAll((x) => float.Parse(x));
        maneColor = new Color(maneColorVals[0], maneColorVals[1], maneColorVals[2]);

        // Load game properties/triggers
        data = new Dictionary<string, string>();
        foreach (KeyValuePair<string, string> dataProperty in DefaultProperties) {
            data[dataProperty.Key] = Get("player.data" + dataProperty.Key, dataProperty.Value);
        }

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
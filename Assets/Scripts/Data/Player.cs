using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class Player : Singleton<Player> {
    #region Events
    public delegate void HealthChangedHandler(Player instance, int newHealth);
    public event HealthChangedHandler HealthChanged;

    public delegate void DeathHandler(Player instance);
    public event DeathHandler Died;
    #endregion

    #region Enums
    public enum Ability {
        Inspect, BaseShot, RainBeam
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
    public Ability ability = Ability.Inspect;
    public PlayerData data = null;
    public string transitionBorder = string.Empty;
    public string lastMap = string.Empty;
    #endregion

    #region Unity callbacks
    void Awake() {
        Load();
        health = data.hearts * 2;
    }
    #endregion

    #region Save/Load functions
    private bool saving = false;
    public void Save() {
        if (saving) return;
        saving = true;
        XmlSerializer bf = new XmlSerializer(typeof(PlayerData));
        FileStream file = File.Create(Application.persistentDataPath + "/playerData.sav");
        bf.Serialize(file, data);
        file.Close();
        saving = false;
    }

    public void Load() {
        if (File.Exists(Application.persistentDataPath + "/playerData.sav")) {
            XmlSerializer bf = new XmlSerializer(typeof(PlayerData));
            FileStream file = File.Open(Application.persistentDataPath + "/playerData.sav", FileMode.Open);
            data = (PlayerData)bf.Deserialize(file);
            file.Close();
        } else {
            data = new PlayerData();
            data.name = "Unnamed pony";
            data.hearts = 3;
            data.maneStyle = 0;
            data.bodyColor = new Color(1,1,1);
            data.maneColor = new Color(.8f, .4f, .3f);
            data.abilities = new List<Ability>() { Player.Ability.Inspect };
            data.properties = new SerializableDictionary<string, object>();
        }
    }
    #endregion
}

[Serializable]
public class PlayerData {
    public string name;
    public int hearts;
    public int maneStyle;
    public Color bodyColor, maneColor;
    public List<Player.Ability> abilities;
    public SerializableDictionary<string, object> properties;
}
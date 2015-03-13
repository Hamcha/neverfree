using UnityEngine;

public class LayerLimit : MonoBehaviour {
    public bool limitAbility;
    public bool limitNotAbility;
    public Player.Ability ability;
    public bool limitProperty;
    public bool limitNotProperty;
    public string propertyName;

    void Start() {
        if (limitAbility && Player.Instance.data.abilities.Contains(ability)) return;
        if (limitNotAbility && !Player.Instance.data.abilities.Contains(ability)) return;
        if (limitProperty && !Player.Instance.data.properties.ContainsKey(propertyName)) return;
        if (limitNotProperty && Player.Instance.data.properties.ContainsKey(propertyName)) return;
        gameObject.SetActive(false);
    }
}

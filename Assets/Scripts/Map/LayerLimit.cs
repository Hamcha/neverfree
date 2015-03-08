using UnityEngine;

public class LayerLimit : MonoBehaviour {
    public bool limitAbility;
    public bool limitNotAbility;
    public Player.Ability ability;

    void Start() {
        if (limitAbility && Player.Instance.data.abilities.Contains(ability)) return;
        if (limitNotAbility && !Player.Instance.data.abilities.Contains(ability)) return;
        gameObject.SetActive(false);
    }
}

using UnityEngine;

public class LayerLimit : MonoBehaviour {
    public bool limitStance;
    public bool limitNotStance;
    public Player.Stance stance;

    void Start() {
        if (limitStance && Player.Instance.data.stances.Contains(stance)) return;
        if (limitNotStance && !Player.Instance.data.stances.Contains(stance)) return;
        gameObject.SetActive(false);
    }
}

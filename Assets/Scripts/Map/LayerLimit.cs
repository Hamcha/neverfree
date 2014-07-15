using UnityEngine;

public class LayerLimit : MonoBehaviour {
    public int maxLayer, minLayer;

    void Start() {
        if (Scene.instance.layer > maxLayer || Scene.instance.layer < minLayer)
            gameObject.SetActiveRecursively(false);
    }
}

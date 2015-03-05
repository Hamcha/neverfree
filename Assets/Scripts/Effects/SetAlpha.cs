using UnityEngine;

public class SetAlpha : MonoBehaviour {
    public float alpha;
    public bool destroyOnFadeOut = false;

    void Update() {
        Color newc = GetComponent<GUIText>().color;
        newc.a = alpha;
        GetComponent<GUIText>().color = newc;
        if (destroyOnFadeOut && alpha <= 0) Destroy(gameObject);
    }
}

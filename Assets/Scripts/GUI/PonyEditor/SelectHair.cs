using UnityEngine;

public class SelectHair : MonoBehaviour {
    public Editor editor;
    private UIButton button;

    void Start() {
        button = GetComponent<UIButton>();
        button.Clicked += (_) => {

        };
    }
}
using UnityEngine;

public class SelectHair : MonoBehaviour {
    public int hairStyleId = 0;

    private Editor editor;
    private UIButton button;

    void Start() {
        button = GetComponent<UIButton>();
        editor = Editor.instance;

        button.Clicked += (_) => {
            editor.hairStyle = hairStyleId;
        };
    }
}
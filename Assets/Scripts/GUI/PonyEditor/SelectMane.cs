using UnityEngine;

public class SelectMane : MonoBehaviour {
    public int maneStyleId = 0;

    private Editor editor;
    private UIButton button;

    void Start() {
        button = GetComponent<UIButton>();
        editor = Editor.instance;

        button.Clicked += (_) => {
            editor.maneStyle = maneStyleId;
        };
    }
}
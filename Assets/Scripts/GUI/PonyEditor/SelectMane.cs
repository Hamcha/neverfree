using UnityEngine;

public class SelectMane : MonoBehaviour {
    public int maneStyleId = 0;

    public SpriteRenderer body, mane;

    private PonyEditor editor;
    private UIButton button;

    void Start() {
        button = GetComponent<UIButton>();
        editor = PonyEditor.instance;

        button.Clicked += (_) => {
            editor.maneStyle = maneStyleId;
        };

        mane.sprite = editor.storage.maneStyles[maneStyleId];

        editor.ColorChanged += ColorChanged;
        ColorChanged(editor.bodyColor, editor.maneColor);
    }

    private void ColorChanged(Color bodyColor, Color maneColor) {
        body.color = bodyColor;
        mane.color = maneColor;
    }
}
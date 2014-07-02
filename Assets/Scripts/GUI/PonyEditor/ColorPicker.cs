using UnityEngine;

public class ColorPicker : MonoBehaviour {
    public delegate void ColorChangedHandler(ColorPicker instance, Color current);
    public event ColorChangedHandler ColorChanged;

    public float radius = 1;
    public Color currentColor;
    public UIDragContainer dragController;
    public UIDraggable brightnessHandle;

    private SpriteRenderer colorWheel;
    private Texture2D texture;

    void Start() {
        dragController = GetComponent<UIDragContainer>();
        colorWheel = GetComponent<SpriteRenderer>();
        texture = colorWheel.sprite.texture;
    }

    void Update() {
        if (dragController.isDragging || brightnessHandle.isDragging) {
            if (brightnessHandle.isDragging) {
                float vPosition = (brightnessHandle.transform.position.y - brightnessHandle.dragRange.yMin)
                                   / brightnessHandle.dragRange.height;
                vPosition = Mathf.Clamp(vPosition, 0, 1);
                colorWheel.color = new Color(vPosition, vPosition, vPosition, 1);
            }
            Vector3 relPosition = dragController.dragHandle.transform.localPosition / radius;
            Vector2 texPosition = new Vector2(relPosition.x / 2 + 0.5f, relPosition.y / 2 + 0.5f);
            currentColor = texture.GetPixelBilinear(texPosition.x, texPosition.y);
            currentColor *= colorWheel.color;
            currentColor.a = 1;

            if (ColorChanged != null) ColorChanged(this, currentColor);
        }
    }
}

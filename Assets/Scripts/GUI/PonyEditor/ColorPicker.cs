using UnityEngine;

public class ColorPicker : MonoBehaviour {
    public float radius = 1;
    public Color currentColor;
    public UIDragContainer dragController;

    private SpriteRenderer colorWheel;
    private Texture2D texture;

    void Start() {
        dragController = GetComponent<UIDragContainer>();
        colorWheel = GetComponent<SpriteRenderer>();
        texture = colorWheel.sprite.texture;
    }

    void Update() {
        if (dragController.isDragging) {
            Vector3 relPosition = dragController.dragHandle.transform.localPosition / radius;
            Vector2 texPosition = new Vector2(relPosition.x / 2 + 0.5f, relPosition.y / 2 + 0.5f);
            currentColor = texture.GetPixelBilinear(texPosition.x, texPosition.y);
            currentColor.a = 1;
        }
    }
}

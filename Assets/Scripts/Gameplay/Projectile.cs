using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed;
    public float deceleration;
    public float threshold;
    public float angle;

    private Vector3 velocity;
    private SpriteRenderer sprite;

    void Start() {
        velocity += speed * Vector3.right;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update() {
        velocity -= velocity.normalized * deceleration * Time.deltaTime;
        Color color = sprite.color;
        color.a = velocity.magnitude / 2;
        sprite.color = color;
        if (velocity.magnitude < threshold) Destroy(gameObject);
        transform.Translate(velocity * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy" ||
            collision.gameObject.tag == "Walls")
            Destroy(gameObject);
    }
}

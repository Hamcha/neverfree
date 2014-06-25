using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float deceleration;
    public float threshold;
    public float angle;

    public  Vector3 initialVelocity = Vector3.zero;
    private Vector3 velocity;
    private SpriteRenderer sprite;

    void Start()
    {
        velocity += speed * Vector3.right;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        velocity -= velocity.normalized * deceleration * Time.deltaTime;
        Color color = sprite.color;
        color.a = velocity.magnitude;
        sprite.color = color;
        if (velocity.magnitude < threshold) Destroy(gameObject);
        transform.Translate(velocity * Time.deltaTime);
    }
}

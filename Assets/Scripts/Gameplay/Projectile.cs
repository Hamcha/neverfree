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

    void Start()
    {
        //velocity  = Quaternion.AngleAxis(angle, Vector3.forward) * initialVelocity;
        velocity += speed * Vector3.right;
    }

    void Update()
    {
        velocity -= velocity.normalized * deceleration * Time.deltaTime;
        if (velocity.magnitude < threshold) Destroy(gameObject);
        transform.Translate(velocity * Time.deltaTime);
    }
}

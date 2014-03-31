using UnityEngine;
using System.Collections;

public class EnemyFollow : MonoBehaviour 
{
    public Transform target;
    public float speed;
    public float farDistance;

    private Animator ani;
    private float currentDirection;
    private bool tracking;

    void Start()
    {
        // Set everything to zero and initialize components
        currentDirection = 0f;
        ani = GetComponent<Animator>();
        tracking = false;
    }

	void Update() 
    {
        // Reset speed
        ani.SetFloat("Speed", 0);

        // Are we too distant from our target to see it?
        if (Vector3.Distance(transform.position, target.position) > farDistance)
        {
            // Remove from tracker if it is active
            if (tracking) { Scene.tracker.Remove(gameObject); tracking = false; }
            return;
        }

        // Add to tracking if it isn't
        if (!tracking)
        {
            Scene.tracker.Add(gameObject, 20f);
            tracking = true;
        }

        // Calculate following speed
        Vector3 spd = Vector3.Normalize(target.position - transform.position) * speed * Time.deltaTime;
        // Apply speed to position
        transform.position += spd;

        // Set animation parameters
        ani.SetFloat("YAxis", -spd.y+0.001f);
        ani.SetFloat("Speed", spd.magnitude*1000);

        // Flip sprite if needed
        float direction = Mathf.Sign(-spd.x);
        if (currentDirection != direction)
        {
            Vector3 rScale = transform.localScale;
            rScale.x = currentDirection = direction;
            transform.localScale = rScale;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw yellow sphere around enemy to highlight range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, farDistance);
    }

}

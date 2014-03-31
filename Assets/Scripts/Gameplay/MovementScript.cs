using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour 
{
    public float maxSpeed;
    public float damping;
    public float threshold;
    public float stopThreshold;
    public Transform cursorPosition;

    public GameObject projectile;
    public float fireDelay;

    public Scene scene;

    private Vector3 velocity;
    private float angle;
    private float currentDirection;
    private float currentDelay;

    private Animator baseAnimator;

	void Start () 
    {
        currentDirection = 0f;
        currentDelay = 0f;
        baseAnimator = GetComponentInChildren<Animator>();
	}
	
	void Update () 
    {
        // Get Axis data
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        // Set speed if over start threshold
        if (Mathf.Abs(Horizontal) > threshold) velocity.x  = maxSpeed * Horizontal;
        else                                   velocity.x -= velocity.x * damping * Time.deltaTime;
        if (Mathf.Abs(Vertical) > threshold) velocity.y  = maxSpeed * Vertical;
        else                                 velocity.y -= velocity.y * damping * Time.deltaTime;

        // Reset speed if below stop threshold
        if (Mathf.Abs(velocity.x) < stopThreshold) velocity.x = 0f;
        if (Mathf.Abs(velocity.y) < stopThreshold) velocity.y = 0f;

        // Softcap speed to min and max limits
        if (velocity.magnitude < stopThreshold) velocity = Vector3.zero;
        if (velocity.magnitude > maxSpeed)      velocity = Vector3.Normalize(velocity) * maxSpeed;

        // Set animation
        baseAnimator.SetFloat("Speed", velocity.magnitude);
        transform.Translate(velocity * Time.deltaTime);

        // Flip Sprite if horizontal direction changed
        float direction = Mathf.Sign(cursorPosition.position.x - transform.position.x);
        if (currentDirection != direction)
        {
            Vector3 rScale = transform.localScale;
            rScale.x = currentDirection = direction;
            transform.localScale = rScale;
        }

        // TODO: Move global delays to Stance-specific ones

        // Shoot if we can and want to
        if (Input.GetMouseButton(0) && Player.Instance.stance == Player.Stance.STANCE_BASE_SHOT)
            if (currentDelay <= 0f) shoot();

        // Change stance if we're USING OMG THE MOUSE WHEEL HAMCHA WHAT WERE YOU THINKING
        // 0/0 would not let design kbm controls again
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            scene.stanceBar.Next();
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            scene.stanceBar.Back();

        // Decrease fire delay
        if (currentDelay > 0f) currentDelay -= Time.deltaTime;
	}

    void shoot()
    {
        float deltaX = cursorPosition.position.x - transform.position.x;
        float deltaY = cursorPosition.position.y - transform.position.y;
        angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;
        GameObject proj = (GameObject)Instantiate(projectile, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        proj.GetComponent<Projectile>().angle = angle;
        proj.GetComponent<Projectile>().initialVelocity = velocity;
        currentDelay = fireDelay;
    }
}

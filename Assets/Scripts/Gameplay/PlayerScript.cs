using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public float maxSpeed;
    public float damping;
    public float threshold;
    public float stopThreshold;
    public float damageBounceDistance;
    public Transform cursorPosition;
    public float protectDelay;

    public bool isProtected { get { return currentProtectDelay > 0; } }
    public static PlayerScript instance { get; private set; }
    public static GameObject player { get { return instance != null ? instance.gameObject : null; } }

    public GameObject baseSprite, maneSprite;
    public Teleport teleporter;
    public Animator baseAnimator;
    public GameObject startCutscene;

    public bool disabled = false;

    private Vector3 velocity;
    private float angle;
    private float currentDirection;
    private float currentProtectDelay;

    private bool canTeleport = false;
    private float teleportRange;

    private static KeyCode[] keys = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, 
                                                    KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, 
                                                    KeyCode.Alpha9, KeyCode.Alpha0 };

    void Start() {
        DontDestroyOnLoad(gameObject);
        instance = this;

        // Don't do the wakeup cutscene during development/testing
        if (!Application.isEditor) {
            Instantiate(startCutscene);
        }

        currentDirection = 0f;
        baseAnimator = GetComponentInChildren<Animator>();
        cursorPosition = Scene.gui.cursor.transform;

        if (Player.Instance.data.properties.ContainsKey("teleportRange")) {
            teleportRange = (float)Player.Instance.data.properties["teleportRange"];
            canTeleport = teleportRange > 0;
        }

        // Load customized pony
        SpriteRenderer mane = maneSprite.GetComponent<SpriteRenderer>();
        mane.sprite = Scene.instance.storage.maneStyles[Player.Instance.data.maneStyle];
        mane.color = Player.Instance.data.maneColor;
        SpriteRenderer body = baseSprite.GetComponent<SpriteRenderer>();
        body.color = Player.Instance.data.bodyColor;
    }

    void Update() {
        // Don't do anything if we're in a cutscene
        if (disabled) return;

        // Get Axis data
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        // Set speed if over start threshold
        if (Mathf.Abs(Horizontal) > threshold) velocity.x = maxSpeed * Horizontal;
        else velocity.x -= velocity.x * damping * Time.deltaTime;
        if (Mathf.Abs(Vertical) > threshold) velocity.y = maxSpeed * Vertical;
        else velocity.y -= velocity.y * damping * Time.deltaTime;

        // Reset speed if below stop threshold
        if (Mathf.Abs(velocity.x) < stopThreshold) velocity.x = 0f;
        if (Mathf.Abs(velocity.y) < stopThreshold) velocity.y = 0f;

        // Softcap speed to min and max limits
        if (velocity.magnitude < stopThreshold) velocity = Vector3.zero;
        if (velocity.magnitude > maxSpeed) velocity = Vector3.Normalize(velocity) * maxSpeed;

        // Set animation
        baseAnimator.SetFloat("Speed", velocity.magnitude);
        transform.Translate(velocity * Time.deltaTime);

        // Flip Sprite if horizontal direction changed
        float direction = Mathf.Sign(cursorPosition.position.x - transform.position.x);
        if (currentDirection != direction) {
            Vector3 rScale = transform.localScale;
            rScale.x = currentDirection = direction;
            transform.localScale = rScale;
        }

        // Shoot if we can and want to
        if (Input.GetMouseButton(0) && Ability.all[Player.Instance.ability].canShoot)
            shoot();
        // Prepare teleportation
        if (canTeleport && Input.GetMouseButton(1)) {
            teleporter.teleporting = true;
            teleporter.range.ScaleTo(teleportRange, 0.5f);
        } else {
            if (teleporter.teleporting) teleporter.TeleportPlayer(gameObject);
            teleporter.range.Immediate(0);
        }

        // Change ability using the Mouse wheel
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            Scene.gui.abilityBar.Next();
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            Scene.gui.abilityBar.Back();
        // Change ability using the numeric keys (0,1,2,3,4..)
        for (int i = 0; i < keys.Length && i < Scene.gui.abilityBar.abilities.Count; i++) {
            if (Input.GetKey(keys[i])) {
                Scene.gui.abilityBar.Raise(i);
            }
        }

        // Invulnerable semi-transparency
        if (currentProtectDelay > 0f) {
            Color baseColor = baseSprite.GetComponent<Renderer>().material.color;
            baseColor.a = Mathf.Floor(currentProtectDelay * 10f) % 2 == 1 ? 0.5f : 1f;
            baseSprite.GetComponent<Renderer>().material.color = baseColor;
            maneSprite.GetComponent<Renderer>().material.color = baseColor;
            currentProtectDelay -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // Bouncing when hit
        if (collision.gameObject.tag == "Enemy") {
            GetComponent<Rigidbody2D>().AddForce(collision.contacts[0].normal * damageBounceDistance, ForceMode2D.Impulse);
            // Not invulnerable? Get hit!
            if (!isProtected) {
                currentProtectDelay = protectDelay;
                Player.Instance.health -= 1;
            }
        }
    }

    void shoot() {
        float deltaX = cursorPosition.position.x - transform.position.x;
        float deltaY = cursorPosition.position.y - transform.position.y;
        angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;
        ((OffensiveAbility)Ability.all[Player.Instance.ability]).Shoot(transform.position, angle);
    }

    void OnDestroy() {
        if (instance == this) instance = null;
    }
}

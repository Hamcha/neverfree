using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public delegate void DeathHandler(Enemy instance);
    public event DeathHandler Died;

    public float initialHealth = 5;
    public float protectDelay = 0.5f;
    public float kickback = 4f;

    public float health {
        get { return _health; }
        set {
            _health = value;
            if (_health <= 0 && Died != null) Died(this);
        }
    }

    public bool isProtected { get { return currentProtectDelay > 0; } }

    private float _health;
    private float currentProtectDelay;

    void Start() {
        _health = initialHealth;
        Died += (obj) => Destroy(gameObject);
    }

    void Update() {
        if (currentProtectDelay > 0) {
            Color baseColor = renderer.material.color;
            baseColor.a = Mathf.Floor(currentProtectDelay * 10f) % 2 == 1 ? 0.5f : 1f;
            renderer.material.color = baseColor;
            currentProtectDelay -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "PlayerProjectile") {
            rigidbody2D.AddForce(collision.contacts[0].normal * kickback, ForceMode2D.Impulse);
            if (!isProtected) {
                currentProtectDelay = protectDelay;
                health--;
            }
        }
    }

    void OnDestroy() {
        Scene.tracker.Remove(gameObject);
    }
}

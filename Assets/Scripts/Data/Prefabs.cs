using UnityEngine;

public class Prefabs : MonoBehaviour {
    public static Prefabs instance { get; private set; }

    public GameObject baseShotProjectile;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        instance = this;

        // Setup projectiles
        ((BaseShotStance)Stance.all[Player.Stance.BaseShot]).projectile = baseShotProjectile;
    }

    void OnDestroy() {
        if (instance == this) instance = null;
    }

    void Update() {
        // Update stances
        foreach (Stance s in Stance.all.Values) {
            s.Update();
        }
    }
}

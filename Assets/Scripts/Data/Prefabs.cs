using UnityEngine;

public class Prefabs : MonoBehaviour {
    public static Prefabs instance { get; private set; }

    public GameObject baseShotProjectile;
    public Sprite[] maneStyles;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        instance = this;

        // Setup projectiles
        ((BaseShotAbility)Ability.all[Player.Ability.BaseShot]).projectile = baseShotProjectile;
    }

    void OnDestroy() {
        if (instance == this) instance = null;
    }

    void Update() {
        // Update stances
        foreach (Ability s in Ability.all.Values) {
            s.Update();
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

class Ability {
    public static Dictionary<Player.Ability, Ability> all = new Dictionary<Player.Ability, Ability> {
        { Player.Ability.Inspect, new InspectAbility() },
        { Player.Ability.BaseShot, new BaseShotAbility() }
    };
    public bool canShoot = false;
    public virtual void Update() { }
}

class OffensiveAbility : Ability {
    protected float fireDelay = 0.5f;
    protected float currentDelay = 0f;

    public OffensiveAbility() {
        canShoot = true;
    }

    public override void Update() {
        if (currentDelay > 0) {
            currentDelay -= Time.deltaTime;
        }
    }

    public virtual void Shoot(Vector2 origin, float angle) {
        currentDelay = fireDelay;
    }

    public virtual bool CanShoot() {
        return currentDelay <= 0;
    }
}

class InspectAbility : Ability {
    public override void Update() {
        base.Update();
    }
}

class BaseShotAbility : OffensiveAbility {
    public GameObject projectile;

    public BaseShotAbility() {
        fireDelay = 0.7f;
    }

    public override void Update() {
        base.Update();
    }

    public override void Shoot(Vector2 origin, float angle) {
        if (!CanShoot()) return;
        GameObject proj = (GameObject)GameObject.Instantiate(projectile, origin, Quaternion.AngleAxis(angle, Vector3.forward));
        proj.GetComponent<Projectile>().angle = angle;
        base.Shoot(origin, angle);
    }
}
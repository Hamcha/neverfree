using System.Collections.Generic;
using UnityEngine;

class Stance {
    public static Dictionary<Player.Stance, Stance> all = new Dictionary<Player.Stance, Stance> {
        { Player.Stance.Inspect, new InspectStance() },
        { Player.Stance.BaseShot, new BaseShotStance() }
    };
    public bool canShoot = false;
    public virtual void Update() { }
}

class OffensiveStance : Stance {
    protected float fireDelay = 0.5f;
    protected float currentDelay = 0f;

    public OffensiveStance() {
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

class InspectStance : Stance {
    public override void Update() {
        base.Update();
    }
}

class BaseShotStance : OffensiveStance {
    public GameObject projectile;

    public BaseShotStance() {
        fireDelay = 0.7f;
    }

    public override void Update() {
        base.Update();
    }

    public override void Shoot(Vector2 origin, float angle) {
        GameObject proj = (GameObject)GameObject.Instantiate(projectile, origin, Quaternion.AngleAxis(angle, Vector3.forward));
        proj.GetComponent<Projectile>().angle = angle;
        base.Shoot(origin, angle);
    }
}
package physics;

import differ.data.ShapeCollision;

class CollisionData {
	public var collided(get, null): Bool;
	public var collision: ShapeCollision = null;

	public function new(collision: ShapeCollision) {
		this.collision = collision;
	}

	public function get_collided(): Bool {
		return collision != null;
	}
}

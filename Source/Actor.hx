package;

import graphics.Scene;
import physics.Collider;
import physics.CollisionData;
import physics.NullCollider;
import physics.ICollidable;
import openfl.display.Sprite;

class Actor extends Sprite implements ICollidable {
	public var collider: Collider;
	public var scene: Scene;

	public function new() {
		collider = new NullCollider();
		super();
	}

	public function collides(collider: Collider): CollisionData {
		updateCollider();
		return collider.test(collider);
	}

	public function getCollider(): Collider {
		updateCollider();
		return collider;
	}

	private function updateCollider() {
		collider.setOffset(x, y);
		collider.setRotation(rotation);
	}
}

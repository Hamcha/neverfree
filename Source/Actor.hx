package;

import physics.Collider;
import physics.CollisionData;
import physics.NullCollider;
import physics.ICollidable;
import openfl.display.Sprite;

class Actor extends Sprite implements ICollidable {
	public var collider: Collider;

	public function new() {
		collider = new NullCollider();
		super();
	}

	public function collides(collider: Collider): CollisionData {
		if (this.collider == null || collider == null) {
			return new CollisionData(null);
		}
		updateCollider();
		return new CollisionData(this.collider.shape.test(collider.shape));
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

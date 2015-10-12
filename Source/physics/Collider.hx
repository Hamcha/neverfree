package physics;

import differ.shapes.Shape;

class Collider {
	public var shape(default, null): Shape;

	public function setOffset(x: Float, y: Float) {
		if (shape == null) {
			return;
		}
		shape.x = x;
		shape.y = y;
	}

	public function setRotation(rotation: Float) {
		if (shape == null) {
			return;
		}
		shape.rotation = rotation;
	}

	public function test(collider: Collider): CollisionData {
		if (shape == null || collider == null || collider.shape == null) {
			return new CollisionData(null);
		}
		return new CollisionData(shape.test(collider.shape));
	}
}

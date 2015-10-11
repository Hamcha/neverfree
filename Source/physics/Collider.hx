package physics;

import differ.ShapeDrawer;
import differ.shapes.Shape;

class Collider {
	private var debugDrawer: ShapeDrawer;
	public var shape(default, null): Shape;

	public function new() {
		debugDrawer = new ShapeDrawer();
	}

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
		debugDrawer.drawShape(shape);
		debugDrawer.drawShape(collider.shape);
		return new CollisionData(shape.test(collider.shape));
	}
}

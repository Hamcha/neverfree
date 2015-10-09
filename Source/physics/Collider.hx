package physics;

import differ.shapes.Shape;

class Collider {
	public var shape(default, null): Shape;

	public function setOffset(x: Float, y: Float) {
		shape.x = x;
		shape.y = y;
	}

	public function setRotation(rotation: Float) {
		shape.rotation = rotation;
	}
}

package physics;

import differ.shapes.Shape;

class NullCollider extends Collider {
	public function new() {}

	public function getCollisionShape(): Shape {
		return null;
	}

	public override function setOffset(x: Float, y: Float): Void {}
	public override function setRotation(rot: Float): Void {}
}

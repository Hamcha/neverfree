package physics;

class NullCollider extends Collider {
	public override function test(collider: Collider): CollisionData {
		return new CollisionData(null);
	}

	public override function setOffset(x: Float, y: Float): Void {}
	public override function setRotation(rot: Float): Void {}
}

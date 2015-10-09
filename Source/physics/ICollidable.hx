package physics;

interface ICollidable {
	public function collides(collider: Collider): CollisionData;
	public function getCollider(): Collider;
}

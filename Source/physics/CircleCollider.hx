package physics;

import openfl.geom.Point;

class CircleCollider implements ICollidable {
	public var size: Float;

	public function new(size: Float) {
		this.size = size;
	}

	public function collides(point: Point): Bool {
		return point.length <= size;
	}
}

package physics;

import openfl.geom.Point;

class NullCollider implements ICollidable {
	public function new() {}
	public function collides(point: Point): Bool {
		return false;
	}
}

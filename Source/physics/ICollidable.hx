package physics;

import openfl.geom.Point;

interface ICollidable {
	public function collides(point: Point): Bool;
}

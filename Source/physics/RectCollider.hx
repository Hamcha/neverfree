package physics;

import openfl.geom.Rectangle;
import openfl.geom.Point;

class RectCollider implements ICollidable {
	public var rect: Rectangle;

	public function new(rect: Rectangle) {
		this.rect = rect;
	}

	public function collides(point: Point): Bool {
		return rect.containsPoint(point);
	}
}

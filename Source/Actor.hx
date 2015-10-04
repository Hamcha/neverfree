package;

import openfl.display.Sprite;
import openfl.geom.Point;
import physics.NullCollider;
import physics.ICollidable;

class Actor extends Sprite implements ICollidable {
	public var collider: ICollidable;

	public function new() {
		collider = new NullCollider();
		super();
	}

	public function collides(point: Point): Bool {
		point.offset(-x, -y);
		return collider.collides(point);
	}
}

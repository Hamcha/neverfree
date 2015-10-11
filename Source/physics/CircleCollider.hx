package physics;

import differ.shapes.Shape;
import differ.shapes.Circle;

class CircleCollider extends Collider {
	public function new(radius: Float) {
		super();
		shape = new Circle(0, 0, radius);
	}
}

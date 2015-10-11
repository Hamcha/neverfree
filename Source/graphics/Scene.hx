package graphics;

import flash.display.DisplayObject;
import physics.ICollidable;
import physics.CollisionData;
import openfl.display.Sprite;

class Scene extends Sprite {
	public var camera: Camera;

	public function new() {
		super();
		camera = new Camera(this);
	}

	public function collides(actor: Actor, category: String): Array<Collision> {
		var collisions = new Array<Collision>();

		// Loop through childrens
		for (cid in 0...numChildren) {
			var children: DisplayObject = getChildAt(cid);

			// Can't collide with self
			if (children == actor) {
				continue;
			}

			// Only check for collidable objects
			if (!Std.is(children, ICollidable)) {
				continue;
			}

			var collidable: ICollidable = cast(children, ICollidable);
			var collisionData: CollisionData = collidable.collides(collidable.getCollider());

			if (collisionData != null && collisionData.collided) {
				collisions.push(new Collision(collisionData, actor, collidable));
			}
		}

		return collisions;
	}

	public function add(actor: Actor) {
		actor.scene = this;
		addChild(actor);
	}
}

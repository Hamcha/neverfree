package graphics;

import utils.DebugShapeDrawer;
import differ.shapes.Shape;
import openfl.events.Event;
import differ.ShapeDrawer;
import flash.display.DisplayObject;
import physics.ICollidable;
import physics.CollisionData;
import openfl.display.Sprite;

class Scene extends Sprite {
	private var debugDrawer: DebugShapeDrawer;
	public var camera: Camera;

	public function new() {
		super();
		camera = new Camera(this);

		var debugLayer: Sprite = new Sprite();
		debugDrawer = new DebugShapeDrawer(debugLayer.graphics);
		addChild(debugLayer);

		// Setup update event
		addEventListener(Event.ADDED_TO_STAGE, function(e: Event){
			stage.addEventListener(Event.ENTER_FRAME, update);
		});
	}

	public function update(e: Event) {
		// Draw all collision shapes
		for (cid in 0...numChildren) {
			var children: DisplayObject = getChildAt(cid);
			// Only check for collidable objects
			if (!Std.is(children, ICollidable)) {
				continue;
			}
			var collidable: ICollidable = cast(children, ICollidable);
			var shape: Shape = collidable.getCollider().shape;

			// Only draw existing shapes
			if (shape != null) {
				debugDrawer.drawShape(shape);
				trace("Drawing " + shape);
			}
		}
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
			var collisionData: CollisionData = collidable.collides(actor.getCollider());

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
